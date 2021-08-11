using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants.AnnouncementConstants;


namespace BookBeing.Data.Models
{
    public class Announcement
    {
        public int Id { get; init; }

        [MaxLength(MaxLenghtText)]
        [MinLength(MinLenghtText)]
        [Required]
        public string Text { get; set; }

        [Required]
        public int LibraryId { get; set; }
        public Library Library { get; set; }

    }
}
