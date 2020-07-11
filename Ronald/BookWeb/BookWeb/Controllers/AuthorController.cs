﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookWeb.Models;
using BookWeb.Interface;
using BookWeb.Entities;
using Microsoft.AspNetCore.Identity;
using BookWeb.Enums;

namespace BookWeb.Controllers
{
    public class AuthorController : BaseController
    {
        private IAuthor _author;

        private readonly UserManager<ApplicationUser> _userManager;
        public AuthorController(IAuthor author, UserManager<ApplicationUser> userManager)
        {
            _author = author;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _author.GetAll();

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
        public async Task<IActionResult> Create(Author author)
        {
            author.CreatedBy = _userManager.GetUserName(User);
            author.DateCreated = DateTime.Now;
            var createAuthor = await _author.AddAsync(author);

            //if (createAuthor)
            //{
            //    return RedirectToAction("Index");
            //}

            if (createAuthor)
            {
                Alert("Author created successfully😃.", NotificationType.success);
                return RedirectToAction("Index","Home");
            }
            else
            {
                Alert("Author not created😔!", NotificationType.error);
            }


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editAuthor = await _author.GetById(id);

            if (editAuthor == null)
            {
                return RedirectToAction("Index");
            }
            return View(editAuthor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {

            var editAuthor = await _author.Update(author);

            if (editAuthor && ModelState.IsValid)
            {
                Alert("Author edited successfully😃.", NotificationType.success);
                return RedirectToAction("Index");
            }
            else
            {
                Alert("Author not edited😔.", NotificationType.error);
            }
            return View();
        }


        public async Task<IActionResult> Delete(int id)
        {
            var deleteAuthor = await _author.Delete(id);
            if (deleteAuthor)
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