using BookBeing.Data;
using BookBeing.Models.Books;
using BookBeing.Services.Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services
{
    public class BookService : IBookService
    {
        private readonly BookBeingDbContext data;

        public BookService(BookBeingDbContext data)
        {
            this.data = data;
        }

        public BookQueryServiceModel All(
            string category,
            string searchTerms,
            BooksSorting sorting,
            int currentPage,
            int booksPerPage)
        {
            var booksQuery = this.data.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(category))
            {
                booksQuery = booksQuery
                    .Where(b => b.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerms))
            {
                booksQuery = booksQuery
                    .Where(b => (
                    b.Title + " " + b.Author.Name + " " + b.Publisher.Name).ToLower().Contains(searchTerms.ToLower()) ||
                    b.Description.ToLower().Contains(searchTerms.ToLower()));
            }

            booksQuery = sorting switch
            {
                BooksSorting.DateCreated => booksQuery.OrderByDescending(b => b.Id),
                BooksSorting.Author => booksQuery.OrderByDescending(b => b.Author.Name),
                BooksSorting.Publisher => booksQuery.OrderByDescending(b => b.Publisher.Name),
                BooksSorting.PriceLow => booksQuery.OrderBy(b => b.Price),
                BooksSorting.PriceHigh => booksQuery.OrderByDescending(b => b.Price),
                _ => booksQuery.OrderByDescending(b => b.Id)
            };

            var countBooks = booksQuery.Count();

            var books = booksQuery
                .Skip((currentPage - 1) * booksPerPage)
                .Take(booksPerPage)
                .Where(b => b.Taken == false)
                .Select(b => new BookServiceModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    ImageUrl = b.ImageUrl,
                    Price = b.Price,
                    Description = b.Description
                })
                .ToList();

            return new BookQueryServiceModel
            {
                BooksPerPage = booksPerPage,
                CurrentPage = currentPage,
                CountBooks = countBooks,
                Books = books
            };
        }

        public IEnumerable<string> AllBookCatrgories()
        {
            return this.data.Categories.Select(x => x.Name).ToList();
        }
    }
}
