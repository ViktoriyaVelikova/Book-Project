﻿using BookBeing.Data;
using BookBeing.Data.Models;
using BookBeing.Infrastructure;
using BookBeing.Models.Books;
using BookBeing.Services.Books;
using BookBeing.Services.Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace BookBeing.Controllers
{
    public class BooksController : Controller
    {
        //The goal is to get out "BookBeingDbContext data" => move the others usages in service
        private readonly BookBeingDbContext data;
        private readonly IBookService books;

        public BooksController(BookBeingDbContext data, IBookService books)
        {
            this.data = data;
            this.books = books;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new BookFormModel
            {
                Categories = this.books.GetBooksCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(BookFormModel book)
        {

            if (!books.CategoryExist(book.CategoryId))
            {
                this.ModelState.AddModelError(nameof(book.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                book.Categories = this.books.GetBooksCategories();
                return View(book);
            }
            var category = this.data.Categories.FirstOrDefault(x => x.Id == book.CategoryId);


            this.books.Create(
                book.Title,
                book.Author,
                book.Publisher,
                book.ImageUrl,
                book.Description,
                book.Category,
                book.Price,
                this.User.GetId());

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllBooksQueryModel query)
        {

            var queryResult = this.books.All(
                query.Category,
                query.SearchTerms,
                query.Sorting,
                query.CurrentPage,
                AllBooksQueryModel.booksPerPage);

            var booksCategories = this.books.AllBookCatrgories();

            query.Categories = booksCategories;
            query.CountBooks = queryResult.CountBooks;
            query.Books = queryResult.Books;

            return View(query);
        }


        [Authorize]
        public IActionResult MyBooks()
        {
            var myBooks = this.books.AllBooksByUser(this.User.GetId());
            return View(myBooks);

        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var theBook = this.books.Details(id);
            if (theBook.UserId != this.User.GetId())
            {
                return Unauthorized();
            }

            return View(new BookFormModel
            {
                Title = theBook.Title,
                Author = theBook.Author,
                Publisher = theBook.Publisher,
                ImageUrl = theBook.ImageUrl,
                Description = theBook.Description,
                Price = theBook.Price,
                CategoryId = theBook.CategoryId,
                Category = theBook.Category,
                Categories = this.books.GetBooksCategories()
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, BookFormModel book)
        {
            if (!books.CategoryExist(book.CategoryId))
            {
                this.ModelState.AddModelError(nameof(book.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                book.Categories = this.books.GetBooksCategories();
                return View(book);
            }
            var category = this.data.Categories.FirstOrDefault(x => x.Id == book.CategoryId);
            if (!books.BookIsByUser(this.User.GetId(), id))
            {
                return BadRequest();
            }
            this.books.Edit(
                 id,
                 book.Title,
                 book.Author,
                 book.Publisher,
                 book.ImageUrl,
                 book.Description,
                 book.Category,
                 book.Price);

            return RedirectToAction(nameof(All));
        }


    }
}
