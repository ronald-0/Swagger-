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

namespace BookWeb.Controllers
{
    public class GenreController : BaseController
    {
        private IGenre _genre;
        private readonly UserManager<ApplicationUser> _userManager;

        public GenreController(IGenre genre, UserManager<ApplicationUser> userManager)
        {
            _genre = genre;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _genre.GetAll();

            if (model != null)
                return View(model);
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            genre.CreatedBy = _userManager.GetUserName(User);
            genre.DateCreated = DateTime.Now;
            var createGenre = await _genre.AddAsync(genre);

            if (createGenre)
            {
                Alert("Genre created successfully.", NotificationType.success);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                Alert("Genre not created.", NotificationType.success);

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editGenre = await _genre.GetById(id);

            if (editGenre == null)
            {
                return RedirectToAction("Index");
            }
            return View(editGenre);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Genre genre)
        {
            var editGenre = await _genre.Update(genre);

            if (editGenre && ModelState.IsValid)
            {
                Alert("Genre edited successfully.", NotificationType.success);
                return RedirectToAction("Index");
            }
            else
            {
                Alert("Genre not edited!", NotificationType.error);
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteGenre = await _genre.Delete(id);
            if (deleteGenre)
            {
                return RedirectToAction("Index");
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