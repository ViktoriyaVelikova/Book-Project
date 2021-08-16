using BookBeing.Data.Models;
using BookBeing.Services.Books.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants.BookConstants;

namespace BookBeing.Models.Books
{
    public class BookFormModel
    {

        [Required]
        [StringLength(MaxLenghtTitle, MinimumLength = MinLenghtTitle)]
        public string Title { get; init; }

        [Required]
        public Author Author { get; init; }

        [Required]
        public Publisher Publisher { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [StringLength(int.MaxValue, MinimumLength =  MinLenghtDescription, ErrorMessage = "The minimum length of the description must be at least {2} chars.")]
        public string Description { get; init; }

        [Required]
        [Range(MinPrice,MaxPrice)]
        public decimal Price { get; init; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<BookCategoryServiceModel> Categories { get; set; }
    }
}
