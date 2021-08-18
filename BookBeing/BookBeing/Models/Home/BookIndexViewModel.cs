using BookBeing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Models.Home
{
    public class BookIndexViewModel
    {
        public int Id { get; init; }

        public string Title { get; set; }

        public Author Author { get; set; }

        public Publisher Publisher { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
