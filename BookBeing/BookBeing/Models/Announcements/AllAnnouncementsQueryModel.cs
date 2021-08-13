using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Models.Announcements
{
    public class AllAnnouncementsQueryModel
    {
        public const int AnnouncementsPerPage = 6;
        public int CurrentPage { get; set; } = 1;
        public int CountAnnouncements { get; set; }
        public string SearchTerms { get; set; }
        public IEnumerable<AllAnnouncementsViewModel> Announcements { get; set; }

    }
}
