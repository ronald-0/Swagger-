using System;
using System.Threading.Tasks;
using BookAPI.Dtos;
using BookAPI.Entities;

namespace BookAPI.Interface
{
    public interface IAccount
    {
        Task<bool> CreateUser(ApplicationUser user, string password);

        Task<SignInModel> SignIn(LoginDto loginDetails);
    }
}
