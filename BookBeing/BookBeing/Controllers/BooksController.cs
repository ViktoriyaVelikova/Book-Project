using BookBeing.Data;
using BookBeing.Data.Models;
using BookBeing.Infrastructure;
using BookBeing.Models.Books;
using BookBeing.Services.Books;
using BookBeing.Services.Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return View(new BookFormModel
            {
                Categories = this.books.GetBooksCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(BookFormModel book)
        {
            if (!this.data.Categories.Any(b => b.Id == book.CategoryId))
            {
                this.ModelState.AddModelError(nameof(book.CategoryId), "This category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                book.Categories = this.books.GetBooksCategories();
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


        [Authorize]
        public IActionResult MyBooks()
        {
            var myBooks = this.books.AllBooksByUser(this.User.GetId());
            return View(myBooks);

        }

        //[Authorize]
        //public IActionResult Edit(int id)
        //{

        //}


    }
}
