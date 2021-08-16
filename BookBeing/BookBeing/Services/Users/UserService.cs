using BookBeing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BookBeingDbContext data;

        public UserService(BookBeingDbContext data)
        {
            this.data = data;
        }

        public bool IsLibrary(string Id)
        {
          return  this.data.Libraries.Any(l => l.UserId == Id);
        }

    }
}
