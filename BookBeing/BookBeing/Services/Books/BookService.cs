using BookBeing.Data;
using BookBeing.Data.Models;
using BookBeing.Models.Books;
using BookBeing.Services.Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Books
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

            var books = GetBooks(booksQuery
                .Skip((currentPage - 1) * booksPerPage)
                .Take(booksPerPage)
                .Where(b => b.Taken == false));

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

        public IEnumerable<BookServiceModel> AllBooksByUser(string userId)
        {
            var books = this.data.Books.Where(b => b.UserId == userId);
            return GetBooks(books);
        }

        private static IEnumerable<BookServiceModel> GetBooks(IQueryable<Book> booksQuery)
        {
            return booksQuery.Select(b => new BookServiceModel
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Publisher = b.Publisher,
                ImageUrl = b.ImageUrl,
                Price = b.Price,
                Description = b.Description
            }).ToList();
        }
        public IEnumerable<BookCategoryServiceModel> GetBooksCategories()
        {
            return this.data.Categories
                .Select(c =>
                new BookCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                }
            ).ToList();
        }

        public BookDetailsServiceModel Details(int bookId)
        {
            var book = data.Books.Where(b => b.Id == bookId).Select(b => new BookDetailsServiceModel
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Publisher = b.Publisher,
                CategoryId = b.CategoryId,
                ImageUrl = b.ImageUrl,
                Description = b.Description,
                Price = b.Price,
                UserId = b.UserId

            }).FirstOrDefault();

            return book;
        }

        public bool CategoryExist(int categoryId)
        {
            return this.data.Categories.Any(c => c.Id == categoryId);
        }

        public int Create(string title, Author author, Publisher publisher, string imageUrl, string description, Category category, decimal price, string userId)
        {
            var newBook = new Book
            {
                Title = title,
                Author = author,
                Publisher = publisher,
                ImageUrl = imageUrl,
                Description = description,
                Category = category,
                Price = price,
                Taken = false,
                UserId = userId

            };
            this.data.Books.Add(newBook);
            data.SaveChanges();

            return newBook.Id;
        }

        public bool Edit(int bookId, string title, Author author, Publisher publisher, string imageUrl, string description, Category category, decimal price)
        {
            var book = this.data.Books.Find(bookId);
            if (book == null)
            {
                return false;
            }

            book.Title = title;
            book.Author = author;
            book.Publisher = publisher;
            book.ImageUrl = imageUrl;
            book.Description = description;
            book.Category = category;
            book.Price = price;
            book.Taken = false;
            //book.UserId = userId;


            data.SaveChanges();

            return true;
        }

        public bool BookIsByUser(string userId, int bookId)
        {
            return data.Books.Any(b => b.Id == bookId && b.UserId == userId);
        }
    }
}
