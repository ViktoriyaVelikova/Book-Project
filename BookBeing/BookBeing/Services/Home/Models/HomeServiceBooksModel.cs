using BookBeing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Home.Models
{
    public class HomeServiceBooksModel
    {
        public int Id { get; init; }

        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
