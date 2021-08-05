using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants;

namespace BookBeing.Data.Models
{
    public class Book
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MaxLenghtTitle)]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Publisher { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; init; }

        [Required]
        public bool Taken { get; set; }
    }
}
