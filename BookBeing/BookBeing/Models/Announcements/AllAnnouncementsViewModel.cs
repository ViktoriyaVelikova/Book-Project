using BookBeing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Models.Announcements
{
    public class AllAnnouncementsViewModel
    {
        public int Id { get; init; }
        public int LibraryId { get; set; }
        public Library Library { get; set; }

        public const int AnnouncementsPerPage = 6;
        public int CurrentPage { get; set; } = 1;
    }
}
