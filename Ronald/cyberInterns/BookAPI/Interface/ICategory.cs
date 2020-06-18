﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Entities;

namespace BookAPI.Interface
{
    public interface ICategory
    {
        void Add(Category category);
        Task<bool> AddAsync(Category category);
        Task<bool> Update(Category category);
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
