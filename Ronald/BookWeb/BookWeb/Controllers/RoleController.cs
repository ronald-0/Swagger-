using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWeb.Dtos;
using BookWeb.Entities;
using BookWeb.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookWeb.Controllers
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

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] RoleDtos registerRole)
        {
            ApplicationRole role = new ApplicationRole();
            role.Name = registerRole.Name;
            role.Name = registerRole.Name;

            var newRole = await _role.CreateRole(role);

            if (newRole)
            {
                return Ok(new { message = "Role has been Created" });

            }
            else
            {
                return BadRequest(new { message = "Role failed to be created" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ReadRoles()
        {
            var role = await _role.ReadRoles();
            return Ok(role);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadId(String Id)
        {
            var role = await _role.ReadId(Id);
            return Ok(role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(String Id, [FromBody] ApplicationRole role)
        {
            role.Id = Id;
            var update = await _role.Update(role);

            if (update)
            {
                return Ok("Role has been Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Role details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String Id)
        {
            var deleteRole = await _role.Delete(Id);
            if (deleteRole)
            {
                return Ok("Role has been Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Role details" });
            }
        }
    }
}
