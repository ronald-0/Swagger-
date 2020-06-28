using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWeb.Entities;

namespace BookWeb.Interface
{
    public interface IReaders
    {
        void Add(Readers readers);
        Task<bool> AddAsync(Readers readers);
        Task<bool> Update(Readers readers);
        Task<IEnumerable<Readers>> GetAll();
        Task<Readers> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
