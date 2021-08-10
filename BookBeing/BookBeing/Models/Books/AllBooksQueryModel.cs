using BookBeing.Data.Models;
using System.Collections.Generic;

namespace BookBeing.Models.Books
{
    public class AllBooksQueryModel
    {
        public const int booksPerPage = 6;
        public string Category { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public string SearchTerms { get; set; }
        public BooksSorting Sorting { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int CountBooks { get; set; }
        public IEnumerable<BookListingViewModel> Books { get; set; }

    }
}
