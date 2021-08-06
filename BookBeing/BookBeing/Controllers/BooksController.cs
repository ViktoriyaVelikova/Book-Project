﻿using BookBeing.Data;
using BookBeing.Data.Models;
using BookBeing.Models.Books;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookBeing.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookBeingDbContext data;

        public BooksController(BookBeingDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add()
        {
            return View(new AddBookFormModel
            {
                Categories = GetBooksCategories()
            });
        }

        [HttpPost]
        public IActionResult Add(AddBookFormModel book)
        {
            if (!this.data.Categories.Any(b => b.Id == book.CategoryId))
            {
                this.ModelState.AddModelError(nameof(book.CategoryId), "This category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                book.Categories = this.GetBooksCategories();
                return View(book);
            }

            var newBook = new Book
            {
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                ImageUrl = book.ImageUrl,
                Description = book.Description,
                CategoryId = book.CategoryId,
                Taken = false

            };
            this.data.Books.Add(newBook);
            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All(string searchTerm)
        {
            var carsQuery = this.data.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                carsQuery = carsQuery.Where(b=>b.Title.Contains())
            }

            var books = this.data
                .Books
                .Where(b => b.Taken == false)
                .OrderByDescending(b => b.Id)
                .Select(b => new BookListingViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description
                })
                .ToList();

            return View(new AllBooksQueryModel
            {
                Books = books,
                SearchTerms = searchTerm
            });
        }
        private IEnumerable<BookCategoryViewModel> GetBooksCategories()
        {
            return this.data.Categories
                .Select(c =>
                new BookCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }
            ).ToList();
        }
    }
}
