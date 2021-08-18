using BookBeing.Models.Libraries;
using BookBeing.Services.Libraries.Models;

namespace BookBeing.Services.Libraries
{
    public interface ILibraryServices
    {
        public bool IsLibrary(string userId);
        public int Create(string name, string city, string zipCode, string address, string phoneNumber, string email, string userId);

        //IEnumerable<BookServiceModel> AllAnnouncementsByUser(string userId); To Announcements?

        public bool Edit(string userId, string libraryName, string city, string zipCode, string address, string PhoneNumber, string email);

        LibraryServiceModel LibraryInfo(string userId);
    }
}
