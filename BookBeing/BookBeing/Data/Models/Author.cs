using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants;

namespace BookBeing.Data.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(AuthorMaxLenght)]
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; init; } = new List<Book>();
    }
}
