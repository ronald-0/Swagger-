using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookWeb.Models;
using BookWeb.Interface;
using BookWeb.Entities;
using BookWeb.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookWeb.Controllers
{
    public class BookController : BaseController
    {
        private IBook _book;
        private IAuthor _author;
        private IGenre _genre;

        private readonly UserManager<ApplicationUser> _userManager;
        public BookController(IBook book, IAuthor author, IGenre genre, UserManager<ApplicationUser> userManager)
        {
            _book = book;
            _author = author;
            _genre = genre;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _book.GetAll();

            if (model != null)
                return View(model);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var author = await _author.GetAll();
            var authorList = author.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Title + " " + a.Name
            });


            ViewBag.author = authorList;

            var genre = await _genre.GetAll();
            var genreList = genre.Select(b => new SelectListItem()
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });


            ViewBag.genre = genreList;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Book Book)
        {
            Book.CreatedBy = _userManager.GetUserName(User);
            Book.GenreId = 1;
            Book.DateCreated = DateTime.Now;
            var createBook = await _book.AddAsync(Book);

            //if (createBook)
            //{
            //    return RedirectToAction("Index");
            //}

            if (createBook)
            {
                Alert("Book created successfully.", NotificationType.success);
                return RedirectToAction("Index");
            }
            else
            {
                Alert("Book not created!", NotificationType.error);
            }


            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}