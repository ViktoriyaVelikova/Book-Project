using BookBeing.Data;
using BookBeing.Services.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly BookBeingDbContext data;

        public HomeService(BookBeingDbContext data)
        {
            this.data = data;
        }

        public List<HomeServiceBooksModel> TakeThreeBooks()
        {
            var books = this.data.Books.OrderByDescending(b => b.Id).Take(3);
            List<HomeServiceBooksModel> result = new List<HomeServiceBooksModel>();
            foreach (var book in books)
            {
                var author = this.data.Authors.FirstOrDefault(a => a.AuthorId == book.AuthorId);
                var publisher = this.data.Publishers.FirstOrDefault(p => p.PublisherId == book.PublisherId);
                result.Add(new HomeServiceBooksModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = author,
                    Publisher = publisher,
                    CategoryId = book.CategoryId,
                    Category = book.Category,
                    ImageUrl = book.ImageUrl,
                    Price = book.Price
                });
            }
            return result;
        }
    }
}
