using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWeb.Data;
using BookWeb.Entities;
using BookWeb.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Services
{
    public class ReadersService : IReaders
    {
        private BookWebDataContext _context;
        public ReadersService(BookWebDataContext context)
        {
            _context = context;
        }

        public void Add(Readers readers)
        {
            _context.Add(readers);
            _context.SaveChanges();
        }
        public async Task<bool> AddAsync(Readers readers)
        {
            try
            {
                await _context.AddAsync(readers);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Delete(int Id)
        {
            // find the entity/object
            var readers = await _context.Readers.FindAsync(Id);

            if (readers != null)
            {
                _context.Readers.Remove(readers);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Readers>> GetAll()
        {

            return await _context.Readers.ToListAsync();
        }

        public async Task<Readers> GetById(int Id)
        {
            var readers = await _context.Readers.FindAsync(Id);

            return readers;
        }

        public async Task<bool> Update(Readers readers)
        {
            var aut = await _context.Readers.FindAsync(readers.Id);
            if (aut != null)
            {
                aut.Name = readers.Name;
                aut.Age = readers.Age;
                aut.Email = readers.Email;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }
    }
}
