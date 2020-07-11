using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookWeb.Models;
using BookWeb.Interface;
using BookWeb.Entities;

namespace BookWeb.Controllers
{
    public class RoleController : Controller
    {
        private IRole _role;
        public RoleController(IRole role)
        {
            _role = role;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _role.ReadRoles();

            if (model != null)
            {
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {

            var createRole = await _role.CreateRole(role);


            if (createRole)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var editRole = await _role.ReadId(id);

            if (editRole == null)
            {
                return RedirectToAction("Index");
            }
            return View(editRole);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationRole role)
        {
            var editRole = await _role.Update(role);

            if (editRole && ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Delete(string id)
        {
            var deleteRole = await _role.Delete(id);
            if (deleteRole)
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
