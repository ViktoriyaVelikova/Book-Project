﻿

namespace BookBeing.Models.Books
{
    public class BookListingViewModel
    {
        public int Id { get; init; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

    }
}
