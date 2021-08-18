using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Models.Books
{
    public class BookDetailsModel : BookFormModel
    {
        public int Id { get; set; }
        public bool IsByUser { get; set; }
    }
}
