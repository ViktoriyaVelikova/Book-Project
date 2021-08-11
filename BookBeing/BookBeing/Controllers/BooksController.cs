using BookBeing.Data;
using BookBeing.Data.Models;
using BookBeing.Models.Books;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookBeing.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookBeingDbContext data;

        public BooksController(BookBeingDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add()
        {
            return View(new AddBookFormModel
            {
                Categories = GetBooksCategories()
            });
        }

        [HttpPost]
        public IActionResult Add(AddBookFormModel book)
        {
            if (!this.data.Categories.Any(b => b.Id == book.CategoryId))
            {
                this.ModelState.AddModelError(nameof(book.CategoryId), "This category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                book.Categories = this.GetBooksCategories();
                return View(book);
            }
            var category = this.data.Categories.FirstOrDefault(x => x.Id == book.CategoryId);

            var newBook = new Book
            {
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                ImageUrl = book.ImageUrl,
                Description = book.Description,
                Category = category,
                Price = book.Price,
                Taken = false,
                UserId = this.User.Identity.Name

            };
            this.data.Books.Add(newBook);
            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllBooksQueryModel query)
        {
            var booksQuery = this.data.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                booksQuery = booksQuery
                    .Where(b => b.Category.Name == query.Category);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerms))
            {
                booksQuery = booksQuery
                    .Where(b => (
                    b.Title + " " + b.Author.Name + " " + b.Publisher.Name).ToLower().Contains(query.SearchTerms.ToLower()) ||
                    b.Description.ToLower().Contains(query.SearchTerms.ToLower()));
            }

            booksQuery = query.Sorting switch
            {
                BooksSorting.DateCreated => booksQuery.OrderByDescending(b => b.Id),
                BooksSorting.Author => booksQuery.OrderByDescending(b => b.Author.Name),
                BooksSorting.Publisher => booksQuery.OrderByDescending(b => b.Publisher.Name),
                BooksSorting.PriceLow => booksQuery.OrderBy(b => b.Price),
                BooksSorting.PriceHigh => booksQuery.OrderByDescending(b => b.Price),
                _ => booksQuery.OrderByDescending(b => b.Id)
            };

            var countBooks = booksQuery.Count();

            var books = booksQuery
                .Skip((query.CurrentPage - 1) * AllBooksQueryModel.booksPerPage)
                .Take(AllBooksQueryModel.booksPerPage)
                .Where(b => b.Taken == false)
                .Select(b => new BookListingViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    ImageUrl = b.ImageUrl,
                    Price = b.Price,
                    Description = b.Description
                })
                .ToList();

            var booksCategories = this.data.Categories.Select(c => c.Name).ToList();
            query.Books = books;
            query.CountBooks = countBooks;
            query.Categories = booksCategories;

            return View(query);
        }
        private IEnumerable<BookCategoryViewModel> GetBooksCategories()
        {
            return this.data.Categories
                .Select(c =>
                new BookCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }
            ).ToList();
        }
    }
}
