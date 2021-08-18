using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BookBeing.Data;
using BookBeing.Models;
using BookBeing.Models.Books;
using BookBeing.Services.Home;
using BookBeing.Models.Home;
using System.Collections.Generic;

namespace BookBeing.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService books;

        public HomeController(IHomeService books)
        {
            this.books = books;
        }

        public IActionResult Index()
        {
            var books = this.books.TakeThreeBooks();
            List<BookIndexViewModel> result = new List<BookIndexViewModel>();
            foreach (var book in books)
            {
                result.Add(new BookIndexViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    CategoryId = book.CategoryId,
                    Category = book.Category,
                    ImageUrl = book.ImageUrl,
                    Price = book.Price
                });
            }

            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
