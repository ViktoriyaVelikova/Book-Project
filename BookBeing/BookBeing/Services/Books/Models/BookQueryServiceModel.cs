using System;
using BookBeing.Services.Books;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Books.Models
{
    public class BookQueryServiceModel
    {
        public  int BooksPerPage { get; set; }
        public int CurrentPage { get; set; } 
        public int CountBooks { get; set; }
        public IEnumerable<BookServiceModel> Books { get; set; }
    }
}
