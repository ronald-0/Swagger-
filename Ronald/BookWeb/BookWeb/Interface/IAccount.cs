using System;
using System.Threading.Tasks;
using BookWeb.Dtos;
using BookWeb.Entities;

namespace BookWeb.Interface
{
    public interface IAccount
    {
        Task<bool> CreateUser(ApplicationUser user, string password);

        Task<SignInModel> SignIn(LoginDto loginDetails);
    }
}
