using BookBeing.Data;
using BookBeing.Data.Models;
using BookBeing.Models.Libraries;
using BookBeing.Services.Libraries.Models;
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
        public bool Edit(string userId, string libraryName, string city, string zipCode, string address, string phoneNumber, string email)
        {
            var library = this.data.Libraries.FirstOrDefault(l => l.UserId == userId);
            if (library == null)
            {
                return false;
            }

            library.LibraryName = libraryName;
            library.City = city;
            library.ZipCode = zipCode;
            library.Address = address;
            library.PhoneNumber = phoneNumber;
            library.Email = email;
            data.SaveChanges();

            return true;
        }

        public LibraryServiceModel LibraryInfo(string userId)
        {
            var library = this.data.Libraries.FirstOrDefault(l => l.UserId == userId);

            return new LibraryServiceModel
            {
                Id = library.Id,
                LibraryName = library.LibraryName,
                City = library.City,
                ZipCode = library.ZipCode,
                Address = library.Address,
                PhoneNumber = library.PhoneNumber,
                Email = library.Email
            };
        }
    }
}
