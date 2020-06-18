using System;
namespace BookAPI.Dtos
{
    public class UserDtos
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
