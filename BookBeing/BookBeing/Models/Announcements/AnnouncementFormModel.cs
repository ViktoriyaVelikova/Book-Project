using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants.AnnouncementConstants;
using BookBeing.Data.Models;

namespace BookBeing.Models.Announcements
{
    public class AnnouncementFormModel
    {
        [MaxLength(MaxLenghtText)]
        [MinLength(MinLenghtText)]
        [Required]
        public string Text { get; set; }

    }
}
