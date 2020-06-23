using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookAPI.Dtos;
using BookAPI.Entities;
using BookAPI.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRole _role;
        public RoleController(IRole role)
        {
            _role = role;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleDtos CreateRole)
        {
            ApplicationRole role = new ApplicationRole();

            role.task = CreateRole.Task;
            


            var newRole = await _role.CreateRole(role, CreateRole.Task);
            if (newRole)
                return Ok(new { message = "Role Created" });

            return BadRequest(new { message = "Role not created" });
        }

        [HttpGet]
        public async Task<IActionResult> ReadRole()
        {
            var role = await _role.ReadRole();
            return Ok(role);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ReadRole(int Id)
        {
            var role = await _role.ReadRole(Id);
            return Ok(role);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(string task, [FromBody] ApplicationRole role)
        {
            role.task = task;
            var updateRoles = await _role.Update(role);

            if (updateRoles)
            {
                return Ok("Roles Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Roles details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var deleteRoles = await _role.Delete(Id);
            if (deleteRoles)
            {
                return Ok("Roles Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Roles details" });
            }
        }
    }
}
