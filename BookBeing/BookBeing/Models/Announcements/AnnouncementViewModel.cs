using BookBeing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Models.Announcements
{
    public class AnnouncementViewModel
    {
        public int Id { get; init; }
        public string Text { get; init; }
        public int LibraryId { get; set; }
        public Library Library { get; set; }
        public bool IsByUser { get; set; }
    }
}
