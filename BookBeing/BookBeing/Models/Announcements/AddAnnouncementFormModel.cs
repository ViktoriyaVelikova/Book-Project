using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants.AnnouncementConstants;
using BookBeing.Data.Models;

namespace BookBeing.Models.Announcement
{
    public class AddAnnouncementFormModel
    {
        [MaxLength(MaxLenghtText)]
        [MinLength(MinLenghtText)]
        [Required]
        public string Text { get; set; }

        public Library Library { get; set; }
    }
}
