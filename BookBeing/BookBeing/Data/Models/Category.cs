using System.Collections.Generic;


namespace BookBeing.Data.Models
{
    public class Category
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<Book> Books { get; init; } = new List<Book>();
    }
}
