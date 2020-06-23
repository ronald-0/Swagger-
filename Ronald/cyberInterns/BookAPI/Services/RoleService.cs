using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookAPI.Dtos;
using BookAPI.Entities;
using BookAPI.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using BookAPI.Data;

namespace BookAPI.Services
{
    public class RoleService: IRole
    {
        private BookApiDataContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private IConfiguration _config;
        public async Task<bool> CreateRole(ApplicationRole role, string task)
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
        public async Task<IEnumerable<ApplicationRole>> ReadRole()
        {

            return await _context.Roles.ToListAsync();
        }

        public async Task<ApplicationRole> ReadRole(int Id)
        {
            var role = await _context.Roles.FindAsync(Id);

            return role;
        }
        public async Task<bool> Update(ApplicationRole role)
        {
            var aut = await _context.Roles.FindAsync(role.Id);
            if (aut != null)
            {
                aut.task = role.task;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }
        public async Task<bool> Delete(int Id)
        {
            // find the entity/object
            var role = await _context.Roles.FindAsync(Id);

            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
