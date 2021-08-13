using BookBeing.Data;
using BookBeing.Data.Models;
using BookBeing.Infrastructure;
using BookBeing.Models.Announcement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly BookBeingDbContext data;

        public AnnouncementController(BookBeingDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult AddAnnouncement()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddAnnouncement(AddAnnouncementFormModel AddAnnouncemen)
        {
            var userId = this.User.GetId();
            var library = data.Libraries.FirstOrDefault(l => l.UserId == userId);
            if (library != null)
            {
                var announcement = new Announcement
                {
                    Text= AddAnnouncemen.Text,
                    LibraryId= library.Id
                };
                data.Announcements.Add(announcement);
                data.SaveChanges();
            }
            //Redirect to all
            return View();
        }

        public IActionResult ALL()
        {
            return View();
        }
    }
}
