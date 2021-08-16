using BookBeing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Books.Models
{
    public class BookDetailsServiceModel : BookServiceModel
    {
        public IEnumerable<BookCategoryServiceModel> Categories { get; set; }
        public string UserId { get; set; }
    }
}
