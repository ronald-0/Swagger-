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
    public class GenreController : ControllerBase
    {
        private IGenre _genre;
        public GenreController(IGenre genre)
            {
                _genre = genre;
            }

        [HttpPost]
        public void Post([FromBody] Genre genre)
        {
            _genre.Add(genre);
        }

        [HttpPost("AddGenre")]
        public async Task<IActionResult> AddGenre([FromBody] Genre genre)
        {
            var createGenre = await _genre.AddAsync(genre);

            if (createGenre)
            {
                return Ok("Genre Created");
            }
            else{
                return BadRequest(new { message = "Unable to create Genre details" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _genre.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _genre.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Genre genre)
        {
            genre.Id = id;
            var updateGenre = await _genre.Update(genre);

            if (updateGenre)
            {
                return Ok("Genre Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Genre details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteGenre = await _genre.Delete(id);
            if (deleteGenre)
            {
                return Ok("Genre Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Genre details" });
            }
        }
    }
}
