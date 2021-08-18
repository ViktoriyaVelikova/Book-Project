using BookBeing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Libraries.Models
{
    public class LibraryServiceModel
    {
        public int Id { get; init; }

        public string LibraryName { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }
        //public ApplicationUser User { get; set; }
        //public virtual IEnumerable<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}
