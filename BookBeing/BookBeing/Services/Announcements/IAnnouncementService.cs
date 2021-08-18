using BookBeing.Services.Announcements.Models;

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

        public bool Edit(int announcementId, string text);

        AnnouncementsLibraryServiceModel GetLibraryInfo(int id);

        string GetAnnoncementText(int id);

        public bool IsByUser(string userId, int announcementId);

        public bool DeleteAnnoncement(int id);

    }
}
