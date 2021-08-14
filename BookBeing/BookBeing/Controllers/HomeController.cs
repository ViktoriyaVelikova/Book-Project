using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BookBeing.Data;
using BookBeing.Models;
using BookBeing.Models.Books;


namespace BookBeing.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookBeingDbContext data;

        public HomeController(BookBeingDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            var books = this.data
                .Books
                .Where(b => b.Taken == false)
                .OrderByDescending(b => b.Id)
                .Select(b => new BookListingViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description,
                    Price = b.Price
                })
                .Take(3)
                .ToList();

            return View(books);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
