using BookBeing.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookBeing.Models.Libraries;
using System.Linq;
using BookBeing.Infrastructure;
using BookBeing.Data.Models;
using BookBeing.Services.Libraries;

namespace BookBeing.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryServices libraries;

        public LibraryController(ILibraryServices libraries)
        {
            this.libraries = libraries;
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
            var userExist = libraries.IsLibrary(this.User.GetId());

            if (userExist)
            {
                return RedirectToAction("All", "Announcement");
            }
            if (!ModelState.IsValid)
            {
                return View(library);
            }

            libraries.Create(
                library.LibraryName,
                library.City,
                library.ZipCode,
                library.Address,
                library.PhoneNumber,
                library.Email,
                this.User.GetId());

            return RedirectToAction("All", "Announcement");
        }

        //[Authorize]
        //public IActionResult MyAnnouncements(string userId)
        //{
        //    var userExist = libraries.IsLibrary(this.User.GetId());
        //    if (!userExist)
        //    {
        //        return BadRequest();
        //    }
        //    var library = this.data.Libraries.FirstOrDefault(l => l.UserId == userId);

        //    if (library != null)
        //    {
        //        announcements = library.Announcements.ToList();
        //    }

        //}
    }
}
