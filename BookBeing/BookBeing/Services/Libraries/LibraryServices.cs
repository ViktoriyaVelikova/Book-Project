using BookBeing.Data;
using BookBeing.Data.Models;
using System.Linq;

namespace BookBeing.Services.Libraries
{
    public class LibraryServices : ILibraryServices
    {
        private readonly BookBeingDbContext data;

        public LibraryServices(BookBeingDbContext data)
        {
            this.data = data;
        }

        public bool IsLibrary(string userId)
        {
            return this.data.Libraries.Any(l => l.UserId == userId);

        }

        public int Create(string name, string city, string zipCode, string address, string phoneNumber, string email, string userId)
        {
            var library = new Library
            {
                LibraryName = name,
                City = city,
                ZipCode = zipCode,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                UserId = userId

            };

            this.data.Libraries.Add(library);

            data.SaveChanges();

            return library.Id;
        }

        //IEnumerable<BookCategoryServiceModel> GetBooksCategories(string userId);

    }
}
