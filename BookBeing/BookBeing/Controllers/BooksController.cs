using BookBeing.Data;
using BookBeing.Data.Models;
using BookBeing.Infrastructure;
using BookBeing.Models.Books;
using BookBeing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace BookBeing.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookBeingDbContext data;
        private readonly IBookService books;

        public BooksController(BookBeingDbContext data, IBookService books)
        {
            this.data = data;
            this.books = books;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new AddBookFormModel
            {
                Categories = GetBooksCategories()
            });
        }

        [HttpPost]
        [Authorize]
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
                UserId = this.User.GetId()

            };
            this.data.Books.Add(newBook);
            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllBooksQueryModel query)
        {

            var queryResult = this.books.All(
                query.Category,
                query.SearchTerms,
                query.Sorting,
                query.CurrentPage,
                AllBooksQueryModel.booksPerPage);

            var booksCategories = this.books.AllBookCatrgories();

            query.Categories = booksCategories;
            query.CountBooks = queryResult.CountBooks;
            query.Books = queryResult.Books;

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
