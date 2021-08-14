using BookBeing.Models.Books;
using BookBeing.Services.Books.Models;
using System.Collections.Generic;

namespace BookBeing.Services
{
    public interface IBookService
    {
        public BookQueryServiceModel All(string category,
            string searchTerms,
            BooksSorting sorting,
            int currentPage,
            int booksPerPage);

        IEnumerable<string> AllBookCatrgories();
    }
}
