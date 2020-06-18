using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookAPI.Entities;
using BookAPI.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBook _book;
        public BookController(IBook book)
            {
                _book = book;
            }

        [HttpPost]
        public void Post([FromBody] Book book)
        {
            _book.Add(book);
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var createBook = await _book.AddAsync(book);

            if (createBook)
            {
                return Ok("Book Created");
            }
            else{
                return BadRequest(new { message = "Unable to create Book details" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _book.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _book.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book book)
        {
            book.Id = id;
            var updateBook = await _book.Update(book);

            if (updateBook)
            {
                return Ok("Book Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Book details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteBook = await _book.Delete(id);
            if (deleteBook)
            {
                return Ok("Book Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Book details" });
            }
        }
    }
}
