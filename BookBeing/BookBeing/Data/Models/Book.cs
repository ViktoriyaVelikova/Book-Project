using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BookBeing.Data.DataConstants.BookConstants;

namespace BookBeing.Data.Models
{
    public class Book
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MaxLenghtTitle)]
        [MinLength(MinLenghtTitle)]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [MaxLength(MaxLenghtDescription)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [Range(MinPrice, MaxPrice)]
        public decimal Price { get; set; }

        [Required]
        public bool Taken { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual string UserTakenById { get; set; }
        public virtual ApplicationUser UserTakenBy { get; set; }


    }
}
