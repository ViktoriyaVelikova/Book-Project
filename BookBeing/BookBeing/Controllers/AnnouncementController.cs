using BookBeing.Data;
using BookBeing.Data.Models;
using BookBeing.Infrastructure;
using BookBeing.Models.Announcements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            var userId = this.User.GetId();
            var library = data.Libraries.FirstOrDefault(l => l.UserId == userId);
            if (library == null)
            {
                return RedirectToAction(nameof(LibraryController.RegisterLibrary), "Library");
            }
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
                    Text = AddAnnouncemen.Text,
                    LibraryId = library.Id,
                    Library = library
                };
                data.Announcements.Add(announcement);
                data.SaveChanges();
                return RedirectToAction("All");
            }
            //TODO: If library is Null => you are not a Librari? Probably add text field to the model?
            //TODO: Use any and method bool for "if exist"
            return RedirectToAction(nameof(LibraryController.RegisterLibrary), "Library");
        }

        public IActionResult All([FromQuery] AllAnnouncementsQueryModel query)
        {
            var announcements = this.data.Announcements.AsQueryable();
            var libraries = this.data.Libraries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerms))
            {
                announcements = announcements
                    .Where(a => (
                    a.Library.City + " " + a.Library.LibraryName + " " + a.Library.ZipCode).ToLower().Contains(query.SearchTerms.ToLower()) ||
                    a.Text.ToLower().Contains(query.SearchTerms.ToLower()));
            }

            var countAnnouncements = announcements.Count();

            var announcementsToView = announcements
                .Skip((query.CurrentPage - 1) * AllAnnouncementsQueryModel.AnnouncementsPerPage)
                 .Take(AllAnnouncementsQueryModel.AnnouncementsPerPage)
                 .Select(a => new AllAnnouncementsViewModel
                 {
                     Id = a.Id,
                     LibraryId = a.LibraryId,
                     Library = libraries.FirstOrDefault(x => x.Id == a.Id)
                 })
                .ToList();
            query.Announcements = announcementsToView;
            query.CountAnnouncements = countAnnouncements;


            return View(query);
        }
        //public IActionResult Details(int id)
        //{

        //}
    }
}
