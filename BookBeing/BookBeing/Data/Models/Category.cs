using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookBeing.Data.Models
{
    public class Category
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Book> Books { get; init; } = new List<Book>();
    }
}
