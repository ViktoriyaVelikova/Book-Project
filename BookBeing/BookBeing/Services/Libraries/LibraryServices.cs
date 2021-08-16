using BookBeing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
