using BookBeing.Data;
using BookBeing.Infrastructure;
using BookBeing.Models.Announcements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookBeing.Services.Announcements;

namespace BookBeing.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementService announcements;

        public AnnouncementController(BookBeingDbContext data, IAnnouncementService announcements)
        {
            this.announcements = announcements;
        }

        [Authorize]
        public IActionResult AddAnnouncement()
        {
            var userId = this.User.GetId();
            if (!announcements.IsLibrary(userId))
            {
                return RedirectToAction(nameof(LibraryController.RegisterLibrary), "Library");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddAnnouncement(AnnouncementFormModel AddAnnouncemen)
        {
            if (!ModelState.IsValid)
            {
                return View(AddAnnouncemen);
            }

            var userId = this.User.GetId();

            if (this.announcements.IsLibrary(userId))
            {
                announcements.Create(AddAnnouncemen.Text, userId);
                return RedirectToAction("All");
            }

            return RedirectToAction(nameof(LibraryController.RegisterLibrary), "Library");
        }

        public IActionResult All([FromQuery] AllAnnouncementsQueryModel query)
        {
            var all = announcements.All(query.SearchTerms, query.CurrentPage, AllAnnouncementsQueryModel.AnnouncementsPerPage);
            query.Announcements = all.Announcements;
            query.CountAnnouncements = all.CountAnnouncements;


            return View(query);
        }


        public IActionResult Details(int id)
        {
            var userId = this.User.GetId();
            var text = this.announcements.GetAnnoncementText(id);
            var librariInfo = this.announcements.GetLibraryInfo(id);
            var isByUser = this.announcements.IsByUser(userId, id);
            if (this.User.IsAdmin())
            {
                isByUser = true;
            }
            return View(new AnnouncementViewModel
            {
                Id = id,
                Text = text,
                LibraryId = librariInfo.LibraryId,
                LibraryName = librariInfo.LibraryName,
                LibraryZipCode = librariInfo.LibraryZipCode,
                LibraryCity = librariInfo.LibraryCity,
                LibraryAddress = librariInfo.LibraryAddress,
                LibraryPhoneNumber = librariInfo.LibraryPhoneNumber,
                LibraryEmail = librariInfo.LibraryEmail,
                IsByUser = isByUser
            });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();
            if (!announcements.IsByUser(userId, id))
            {
                return Unauthorized();
            }
            var text = this.announcements.GetAnnoncementText(id);
            return View(new AnnouncementFormModel { Text = text });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, AnnouncementFormModel announcement)
        {
            var userId = this.User.GetId();
            if (!announcements.IsByUser(userId, id))
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return View(announcement);
            }
            this.announcements.Edit(id, announcement.Text);

            return RedirectToAction("Details", "Announcement", new { @id = id });

        }
        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = this.User.GetId();
            if (!announcements.IsByUser(userId, id))
            {
                return Unauthorized();
            }

            this.announcements.DeleteAnnoncement(id);
            return RedirectToAction(nameof(All));
        }
    }
}
