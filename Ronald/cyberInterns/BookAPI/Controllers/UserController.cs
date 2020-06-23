using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookAPI.Dtos;
using BookAPI.Entities;
using BookAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _userService;
        private IConfiguration _config;

        public UserController(IUser userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }
        // GET: /<controller>/
      
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDtos userDto)
        {
            // map dto to entity
           // var userdto = _mapper.Map<User>(userDto);

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Username  = userDto.Username,
                PhoneNo  = userDto.PhoneNo
            };
            try
            {
                // save 
                var userCreated = _userService.Create(user, userDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });



            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =  new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //var claims = new List<Claim>
            //        {
            //        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //        };

            //var tokenDescriptor = new JwtSecurityToken(
            //        issuer: "http://cyberinterns.slack.com",
            //        audience: "http://api.cyberinterns.com",
            //        expires: DateTime.UtcNow.AddDays(7),
            //        cliams: claims,
            //        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            //        );



            


            var token = tokenHandler.CreateToken(tokenDescriptor);

            var Expires = tokenDescriptor.Expires.ToString();





            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                access_token = tokenHandler.WriteToken(token),
                expires = Expires
            });
        }

    }
}
