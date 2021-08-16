using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Users
{
    public interface IUserService
    {
        public bool IsLibrary(string Id);
        //public string UserId(string Id);

    }
}
