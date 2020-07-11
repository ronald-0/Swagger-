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
    public class CategoryController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private ICategory _category;
        public CategoryController(ICategory category, UserManager<ApplicationUser> userManager)
        {
            _category = category;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _category.GetAll();

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
        public async Task<IActionResult> Create(Category category)
        {
            category.CreatedBy = _userManager.GetUserName(User);
            category.DateCreated = DateTime.Now;
            var createCategory = await _category.AddAsync(category);

            if (createCategory)
            {
                Alert("Category created successfully.", NotificationType.success);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Alert("Category not created.", NotificationType.success);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editCategory = await _category.GetById(id);

            if (editCategory == null)
            {
                return RedirectToAction("Index");
            }
            return View(editCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category author)
        {

            var editCategory = await _category.Update(author);

            if (editCategory && ModelState.IsValid)
            {
                Alert("Category edited successfully.", NotificationType.success);
                return RedirectToAction("Index");
            }
            else
            {
                Alert("Category not edited!", NotificationType.error);
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteCategory = await _category.Delete(id);
            if (deleteCategory)
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