using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants.LibraryConstants;

namespace BookBeing.Data.Models
{
    public class Library
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MaxLenghtName)]
        [MinLength(MinLenghtName)]
        public string LibraryName { get; set; }

        [Required]
        [MaxLength(MaxLenghtCity)]
        [MinLength(MinLenghtCity)]
        public string City { get; set; }

        [Required]
        [MaxLength(MaxLenghtZipCode)]
        [MinLength(MinLenghtZipCode)]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(MaxLenghtAddress)]
        [MinLength(MinLenghtAddress)]
        public string Address { get; set; }

        [Required]
        [MaxLength(MaxLenghtPhone)]
        [MinLength(MinLenghtPhone)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual IEnumerable<Announcement> Announcements { get; set; } = new List<Announcement>();

    }
}
