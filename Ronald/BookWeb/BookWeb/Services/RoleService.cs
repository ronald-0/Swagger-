using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookWeb.Dtos;
using BookWeb.Entities;
using BookWeb.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using BookWeb.Data;

namespace BookAPI.Services
{
    public class RoleService: IRole
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private IConfiguration _config;
        public RoleService(
                                RoleManager<ApplicationRole> roleManager,
                                 IConfiguration config)
        {
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<bool> CreateRole(ApplicationRole role)
        {
            try
            {
                var checkRole = await _roleManager.CreateAsync(role);
                if (checkRole.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Delete(string Id)
        {
            var delete = await _roleManager.FindByIdAsync(Id);
            if (delete != null)
            {
                await _roleManager.DeleteAsync(delete);
                return true;
            }
            return false;
        }
        public async Task<List<ApplicationRole>> ReadRoles()
        {

            return await _roleManager.Roles.ToListAsync();
        }
        public async Task<ApplicationRole> ReadId(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);

            return role;
        }
        public async Task<bool> Update(ApplicationRole r)
        {
            var role = await _roleManager.FindByIdAsync(r.Id);
            if (role != null)
            {
                role.Name = r.Name;
                return true;
            }

            return false;
        }
    }
}
