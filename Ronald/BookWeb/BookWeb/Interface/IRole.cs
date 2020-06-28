using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookWeb.Dtos;
using BookWeb.Entities;

namespace BookWeb.Interface
{
    public interface IRole
    {
        Task<bool> CreateRole(ApplicationRole role);
        Task<bool> Delete(string Id);
        Task<List<ApplicationRole>> ReadRoles();
        Task<ApplicationRole> ReadId(string Id);
        Task<bool> Update(ApplicationRole role);
    }
}
