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
    public class ReadersController : BaseController
    {
        private IReaders _readers;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReadersController(IReaders readers, UserManager<ApplicationUser> userManager)
        {
            _readers = readers;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _readers.GetAll();

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
        public async Task<IActionResult> Create(Readers readers)
        {

            var createReaders = await _readers.AddAsync(readers);

            if (createReaders)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editReaders = await _readers.GetById(id);

            if (editReaders == null)
            {
                return RedirectToAction("Index");
            }
            return View(editReaders);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Readers readers)
        {
            var editReaders = await _readers.Update(readers);

            if (editReaders && ModelState.IsValid)
            {
                Alert("Reader edited successfully.", NotificationType.success);
                return RedirectToAction("Index");
            }
            else
            {
                Alert("Reader not edited!", NotificationType.error);
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteReaders = await _readers.Delete(id);
            if (deleteReaders)
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