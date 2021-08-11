using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants.AuthorConstants;

namespace BookBeing.Data.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(MaxLenghtName)]
        [MinLength(MinLenghtName)]
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; init; } = new List<Book>();
    }
}
