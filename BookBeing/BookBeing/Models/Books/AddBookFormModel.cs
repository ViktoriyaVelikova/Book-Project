
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants;

namespace BookBeing.Models.Books
{
    public class AddBookFormModel
    {

        [Required]
        [StringLength(MaxLenghtTitle, MinimumLength = MinLenghtTitle)]
        public string Title { get; init; }

        [Required]
        [StringLength(MaxLenghtAuthor, MinimumLength = MinLenghtAuthor)]
        public string Author { get; init; }

        public string Publisher { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [StringLength(int.MaxValue, MinimumLength = BookDescriptionMinLenght, ErrorMessage = "The minimum length of the description must be at least {2} chars.")]
        public string Description { get; init; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<BookCategoryViewModel> Categories { get; set; }
    }
}
