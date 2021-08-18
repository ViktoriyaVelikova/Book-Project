using BookBeing.Data;
using BookBeing.Data.Models;
using BookBeing.Models.Announcements;
using BookBeing.Services.Announcements;
using BookBeing.Services.Announcements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Announcements
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly BookBeingDbContext data;

        public AnnouncementService(BookBeingDbContext data)
        {
            this.data = data;
        }

        public int Create(string text, string userId)
        {

            var library = this.data.Libraries.FirstOrDefault(l => l.UserId == userId);
            var announcement = new Announcement
            {
                Text = text,
                LibraryId = library.Id,
                Library = library

            };
            data.Announcements.Add(announcement);
            data.SaveChanges();
            return announcement.Id;
        }

        public bool IsLibrary(string userId)
        {
            var chek = this.data.Libraries.Any(l => l.UserId == userId);
            return chek;
        }

        public AnnouncementsQueryServiceModel All(
            string searchTerms,
            int currentPage,
            int announcementsPerPage)
        {
            var announcements = this.data.Announcements.AsQueryable();
            var libraries = this.data.Libraries.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerms))
            {
                announcements = announcements
                    .Where(a => (
                    a.Library.City + " " + a.Library.LibraryName + " " + a.Library.ZipCode).ToLower().Contains(searchTerms.ToLower()) ||
                    a.Text.ToLower().Contains(searchTerms.ToLower()));
            }

            var countAnnouncements = announcements.Count();

            var announcementsToView = announcements
                .Skip((currentPage - 1) * announcementsPerPage)
                .Take(announcementsPerPage)
                .Select(a => new AnnouncementsServiceModel
                {
                    Id = a.Id,
                    LibraryId = a.LibraryId,
                    Library = a.Library
                })
                .ToList();

            return new AnnouncementsQueryServiceModel
            {
                CurrentPage = currentPage,
                AnnouncementsPerPage = announcementsPerPage,
                CountAnnouncements = countAnnouncements,
                Announcements = announcementsToView
            };

        }

        public bool IsByUser(string userId, int announcementId)
        {
            var librariId = this.data.Announcements.FirstOrDefault(a => a.Id == announcementId).LibraryId;
            var library = this.data.Libraries.FirstOrDefault(l => l.Id == librariId);

            return library.UserId == userId;
        }

        public Library GetLibrary(string userId)
        {
            return this.data.Libraries.FirstOrDefault(l => l.UserId == userId);
        }

        public string GetAnnoncementText(int id)
        {
            return this.data.Announcements.FirstOrDefault(a => a.Id == id).Text;
        }

        public bool Edit(int announcementId, string text)
        {
            var announcement = this.data.Announcements.FirstOrDefault(a => a.Id == announcementId);
            if (announcement == null)
            {
                return false;
            }

            announcement.Text = text;
            data.SaveChanges();

            return true;
        }

        public bool DeleteAnnoncement(int id)
        {
            var announcement = this.data.Announcements.FirstOrDefault(a => a.Id == id);
            if (announcement == null)
            {
                return false;
            }

            this.data.Announcements.Remove(announcement);
            data.SaveChanges();

            return true;
        }

    }
}
