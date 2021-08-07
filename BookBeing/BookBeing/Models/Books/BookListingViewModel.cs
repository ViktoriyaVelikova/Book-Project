using BookBeing.Data.Models;

namespace BookBeing.Models.Books
{
    public class BookListingViewModel
    {
        public int Id { get; init; }

        public string Title { get; set; }

        public Author Author { get; set; }

        public Publisher Publisher { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
