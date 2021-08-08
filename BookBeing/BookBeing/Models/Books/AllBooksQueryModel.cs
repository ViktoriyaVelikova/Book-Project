using BookBeing.Data.Models;
using System.Collections.Generic;

namespace BookBeing.Models.Books
{
    public class AllBooksQueryModel
    {
        public string Titles { get; set; }
        public string Category { get; set; }
        public IEnumerable<string> Categoryes { get; set; }
        public string SearchTerms { get; set; }
        public BooksSorting BooksSorting { get; set; }
        public IEnumerable<BookListingViewModel> Books { get; set; }

    }
}
