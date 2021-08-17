using BookBeing.Data.Models;
using BookBeing.Models.Books;
using BookBeing.Services.Books.Models;
using System.Collections.Generic;

namespace BookBeing.Services.Books
{
    public interface IBookService
    {
        public BookQueryServiceModel All(string category,
            string searchTerms,
            BooksSorting sorting,
            int currentPage,
            int booksPerPage);

        IEnumerable<string> AllBookCatrgories();
        IEnumerable<BookServiceModel> AllBooksByUser(string userId);
        IEnumerable<BookCategoryServiceModel> GetBooksCategories();

        Category GetCategory(int categoriId);

        BookDetailsServiceModel Details(int bookId);
        bool CategoryExist(int categoryId);
        bool BookIsByUser(string userId, int bookId);

        int Create(
            string title,
            Author author,
            Publisher publisher,
            string imageUrl,
            string description,
            Category category,
            decimal price,
            string userId);
        bool Edit(
           int bookId,
           string title,
           Author author,
           Publisher publisher,
           string imageUrl,
           string description,
           Category category,
           decimal price);
    }
}
