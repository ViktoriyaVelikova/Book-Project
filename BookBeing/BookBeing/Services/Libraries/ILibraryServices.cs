using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Libraries
{
    public interface ILibraryServices
    {
        public bool IsLibrary(string userId);
    }
}
