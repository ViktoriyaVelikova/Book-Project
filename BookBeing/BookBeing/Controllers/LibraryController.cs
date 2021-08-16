using BookBeing.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookBeing.Models.Libraries;
using System.Linq;
using BookBeing.Infrastructure;
using BookBeing.Data.Models;

namespace BookBeing.Controllers
{
    public class LibraryController : Controller
    {
        private readonly BookBeingDbContext data;

        public LibraryController(BookBeingDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult RegisterLibrary()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public IActionResult RegisterLibrary(AddLibraryFormModel library)
        {
            var userExist = data.Libraries.Any(l => l.UserId == this.User.GetId());
            if (!userExist)
            {
                var newLibrary = new Library
                {
                    LibraryName = library.LibraryName,
                    City = library.City,
                    ZipCode = library.ZipCode,
                    Address = library.Address,
                    PhoneNumber = library.PhoneNumber,
                    Email = library.Email,
                    UserId = this.User.GetId()
                };

                data.Libraries.Add(newLibrary);
                data.SaveChanges();

            }

            //TODO If you are already a Library? 
            return RedirectToAction("All", "Announcement");
        }

        //[Authorize]
        //public IActionResult MyAnnouncements(string userId)
        //{
        //    var library = this.data.Libraries.FirstOrDefault(l => l.UserId == userId);
        //    List<Announcement> announcements = new List<Announcement>();
        //    if (library != null)
        //    {
        //        announcements = library.Announcements.ToList();
        //    }

        //}
    }
}
