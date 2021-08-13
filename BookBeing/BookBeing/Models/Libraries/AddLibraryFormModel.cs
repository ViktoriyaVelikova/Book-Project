using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BookBeing.Data.DataConstants.LibraryConstants;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Models.Libraries
{
    public class AddLibraryFormModel
    {
        [Required]
        [MaxLength(MaxLenghtName)]
        [MinLength(MinLenghtName)]
        [Display(Name = "Library Name")]
        public string LibraryName { get; set; }

        [Required]
        [MaxLength(MaxLenghtCity)]
        [MinLength(MinLenghtCity)]
        public string City { get; set; }

        [Required]
        [MaxLength(MaxLenghtZipCode)]
        [MinLength(MinLenghtZipCode)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(MaxLenghtAddress)]
        [MinLength(MinLenghtAddress)]
        public string Address { get; set; }

        [Required]
        [MaxLength(MaxLenghtPhone)]
        [MinLength(MinLenghtPhone)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
