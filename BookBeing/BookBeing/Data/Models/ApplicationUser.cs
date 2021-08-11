using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static BookBeing.Data.DataConstants.UserConstants;

namespace BookBeing.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(MaxLenghtName)]
        [MinLength(MinLenghtName)]
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
        public virtual IEnumerable<Book> BoughtBooks { get; set; } = new List<Book>();

    }
}
