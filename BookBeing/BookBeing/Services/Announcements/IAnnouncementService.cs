using BookBeing.Data.Models;
using BookBeing.Models.Announcements;
using BookBeing.Services.Announcements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Announcements
{
    public interface IAnnouncementService
    {
        public bool IsLibrary(string userId);
        int Create(string text, string userId);
        public AnnouncementsQueryServiceModel All(
            string searchTerms,
            int currentPage,
            int announcementsPerPage);

        public bool Edit(int announcement, string text);

        Library GetLibrary(string id);

        string GetAnnoncementText(int id);

        public bool IsByUser(string userId, int announcementId);

        public bool DeleteAnnoncement(int id);

    }
}
