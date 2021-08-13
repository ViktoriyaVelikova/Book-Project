using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants.AnnouncementConstants;
using BookBeing.Data.Models;

namespace BookBeing.Models.Announcements
{
    public class AddAnnouncementFormModel
    {
        [MaxLength(MaxLenghtText)]
        [MinLength(MinLenghtText)]
        [Required]
        public string Text { get; set; }

        public int LibraryId { get; set; }
        public Library Library { get; set; }
    }
}
