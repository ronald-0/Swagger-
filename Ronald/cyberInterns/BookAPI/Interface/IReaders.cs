using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Entities;

namespace BookAPI.Interface
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
