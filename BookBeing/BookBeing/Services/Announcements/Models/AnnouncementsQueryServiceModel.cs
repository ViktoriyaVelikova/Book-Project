using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Announcements.Models
{
    public class AnnouncementsQueryServiceModel
    {
        public int AnnouncementsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int CountAnnouncements { get; set; }
        public IEnumerable<AnnouncementsServiceModel> Announcements { get; set; }
    }
}
