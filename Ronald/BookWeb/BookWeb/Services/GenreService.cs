using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWeb.Data;
using BookWeb.Entities;
using BookWeb.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Services
{
    public class GenreService : IGenre
    {
        private BookWebDataContext _context;
        public GenreService(BookWebDataContext context)
        {
            _context = context;
        }

        public void Add(Genre genre)
        {
            _context.Add(genre);
            _context.SaveChanges();
        }
        public async Task<bool> AddAsync(Genre genre)
        {
            try
            {
                await _context.AddAsync(genre);
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
            var genre = await _context.Genres.FindAsync(Id);

            if (genre != null)
            {
                _context.Genres.Remove(genre);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {

            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetById(int Id)
        {
            var genre = await _context.Genres.FindAsync(Id);

            return genre;
        }

        public async Task<bool> Update(Genre genre)
        {
            var aut = await _context.Genres.FindAsync(genre.Id);
            if (aut != null)
            {
                aut.Name = genre.Name;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }
    }
}
