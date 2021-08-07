using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants;

namespace BookBeing.Data.Models
{
    public class Publisher
    {
        public int PublisherId { get; set; }

        [Required]
        [MaxLength(PublisherMaxLenght)]
        public string Name { get; set; }

        public IEnumerable<Book> Books { get; init; } = new List<Book>();
    }
}
