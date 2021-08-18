using BookBeing.Infrastructure;
using BookBeing.Models.Books;
using BookBeing.Services.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookBeing.Controllers
{
    public class BooksController : Controller
    {

        private readonly IBookService books;

        public BooksController(IBookService books)
        {
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

            if (!books.CategoryExist(book.CategoryId))
            {
                this.ModelState.AddModelError(nameof(book.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                book.Categories = this.books.GetBooksCategories();
                return View(book);
            }

            var category = books.GetCategory(book.CategoryId);

            this.books.Create(
                book.Title,
                book.Author,
                book.Publisher,
                book.ImageUrl,
                book.Description,
                book.Category,
                book.Price,
                this.User.GetId());

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllBooksQueryModel query)
        {
            //Goal: all books and coun books
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

        [Authorize]
        public IActionResult Edit(int id)
        {
            var theBook = this.books.Details(id);
            if (theBook.UserId != this.User.GetId() && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new BookFormModel
            {
                Title = theBook.Title,
                Author = theBook.Author,
                Publisher = theBook.Publisher,
                ImageUrl = theBook.ImageUrl,
                Description = theBook.Description,
                Price = theBook.Price,
                CategoryId = theBook.CategoryId,
                Category = theBook.Category,
                Categories = this.books.GetBooksCategories()
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, BookFormModel book)
        {
            if (!books.CategoryExist(book.CategoryId))
            {
                this.ModelState.AddModelError(nameof(book.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                book.Categories = this.books.GetBooksCategories();
                return View(book);
            }

            var category = books.GetCategory(book.CategoryId);

            if (!books.BookIsByUser(this.User.GetId(), id) && !User.IsAdmin())
            {
                return BadRequest();
            }
            this.books.Edit(
                 id,
                 book.Title,
                 book.Author,
                 book.Publisher,
                 book.ImageUrl,
                 book.Description,
                 book.CategoryId,
                 book.Category,
                 book.Price);

            return RedirectToAction(nameof(All));
        }


        public IActionResult Details(int id)
        {
            var book = books.Details(id);

            return View(new BookDetailsModel
            {
                Id = id,
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                ImageUrl = book.ImageUrl,
                Description = book.Description,
                Price = book.Price,
                CategoryId = book.CategoryId,
                Category = book.Category,
                IsByUser = books.BookIsByUser(this.User.GetId(), id)
            });

        }

        [Authorize]
        public IActionResult Buy(int id)
        {
            if (books.BookIsByUser(this.User.GetId(), id) && User.IsAdmin())
            {
                return BadRequest();
            }

            var user = this.User.GetId();
            books.BuyBook(id, user);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!books.BookIsByUser(this.User.GetId(), id) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.books.DeleteBook(id);

            return RedirectToAction(nameof(All));
        }

    }
}
