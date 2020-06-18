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
    public class ReadersController : ControllerBase
    {
        private IReaders _readers;
        public ReadersController(IReaders readers)
            {
                _readers = readers;
            }

        [HttpPost]
        public void Post([FromBody] Readers readers)
        {
            _readers.Add(readers);
        }

        [HttpPost("AddReaders")]
        public async Task<IActionResult> AddReaders([FromBody] Readers readers)
        {
            var createReaders = await _readers.AddAsync(readers);

            if (createReaders)
            {
                return Ok("Readers Created");
            }
            else{
                return BadRequest(new { message = "Unable to create Readers details" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _readers.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _readers.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Readers readers)
        {
            readers.Id = id;
            var updateReaders = await _readers.Update(readers);

            if (updateReaders)
            {
                return Ok("Readers Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Readers details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteReaders = await _readers.Delete(id);
            if (deleteReaders)
            {
                return Ok("Readers Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Readers details" });
            }
        }
    }
}
