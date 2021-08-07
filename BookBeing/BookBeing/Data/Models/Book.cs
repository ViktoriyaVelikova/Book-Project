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
        public int AuthorId { get; set; }
        public Author Author { get; init; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; init; }

        [Required]
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; init; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Range(0,10000)]
        public bool Taken { get; set; }




    }
}
