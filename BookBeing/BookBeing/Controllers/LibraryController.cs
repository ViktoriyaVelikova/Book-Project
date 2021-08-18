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

        [Authorize]
        public IActionResult Edit()
        {
            var userId = this.User.GetId();
            if (!libraries.IsLibrary(userId))
            {
                return RedirectToAction(nameof(LibraryController.RegisterLibrary), "Library");
            }
           var libraryInfo= libraries.LibraryInfo(userId);
            var library = new AddLibraryFormModel
            {
                Address= libraryInfo.Address,
                City= libraryInfo.City,
                Email= libraryInfo.Email,
                LibraryName= libraryInfo.LibraryName,
                PhoneNumber= libraryInfo.PhoneNumber,
                ZipCode= libraryInfo.ZipCode
            };
            return View(library);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, AddLibraryFormModel library)
        {
            var userId = this.User.GetId();
            if (!libraries.IsLibrary(userId))
            {
                return RedirectToAction(nameof(LibraryController.RegisterLibrary), "Library");
            }

            this.libraries.Edit(
                userId,
                library.LibraryName,
                library.City,
                library.ZipCode,
                library.Address,
                library.PhoneNumber,
                library.Email);
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
