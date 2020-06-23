using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookAPI.Dtos;
using BookAPI.Entities;

namespace BookAPI.Interface
{
    public interface IRole
    {
        Task<bool> CreateRole(ApplicationRole role, string task);
        Task<IEnumerable<ApplicationRole>> ReadRole();
        Task<ApplicationRole> ReadRole(int Id);
        Task<bool> Update(ApplicationRole role);
        Task<bool> Delete(int Id);
    }
}
