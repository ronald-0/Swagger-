﻿using System;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private IAccount _account;
        public AccountController(IAccount account)
            {
            _account = account;
            }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDtos registerUser)
        {
            ApplicationUser user = new ApplicationUser();

            user.FirstName = registerUser.FirstName;
            user.LastName = registerUser.LastName;
            user.UserName = registerUser.Username;
            user.Email = registerUser.Email;


            var newUser = await _account.CreateUser(user, registerUser.Password);
            if (newUser)
                return Ok(new { message = "User Created" });

            return BadRequest(new { message = "User not created" });
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto userDto)
        {
            var user = await _account.SignIn(userDto);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

            //[HttpPost("AddAuthor")]
            //public async Task<IActionResult> AddAuthor([FromBody] Author author)
            //{
            //    var createAuthor = await _author.AddAsync(author);

            //    if (createAuthor)
            //    {
            //        return Ok("Author Created");
            //    }
            //    else{
            //        return BadRequest(new { message = "Unable to create Author details" });
            //    }
            //}
            //[AllowAnonymous]
            //[HttpGet]
            //public async Task<IActionResult> GetAll()
            //{
            //    var users = await _author.GetAll();
            //    return Ok(users);
            //}

            //[HttpGet("{id}")]
            //public async Task<IActionResult> GetById(int id)
            //{
            //    var user = await _author.GetById(id);
            //    return Ok(user);
            //}

            //[HttpPut("{id}")]
            //public async Task<IActionResult> Put(int id, [FromBody] Author author)
            //{
            //    author.Id = id;
            //    var updateAuthor = await _author.Update(author);

            //    if (updateAuthor)
            //    {
            //        return Ok("Author Updated");
            //    }
            //    else
            //    {
            //        return BadRequest(new { message = "Unable to update Author details" });
            //    }
            //}

            //// DELETE api/values/5
            //[HttpDelete("{id}")]
            //public async Task<IActionResult> Delete(int id)
            //{
            //    var deleteAuthor = await _author.Delete(id);
            //    if (deleteAuthor)
            //    {
            //        return Ok("Author Deleted");
            //    }
            //    else
            //    {
            //        return BadRequest(new { message = "Unable to delete Author details" });
            //    }
            //}
        }
    }
