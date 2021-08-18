using BookBeing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Announcements.Models
{
    public class AnnouncementsServiceModel
    {
        public int Id { get; init; }
        public int LibraryId { get; set; }
        public Library Library { get; set; }

    }
}
