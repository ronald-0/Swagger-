using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BookAPI.Data;
using BookAPI.Entities;
using BookAPI.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Services
{
    public class BookService : IBook
    {
        private BookApiDataContext _context;
        public BookService(BookApiDataContext context)
        {
            _context = context;
        }

        public void Add(Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
        }
        public async Task<bool> AddAsync(Book book)
        {
            try
            {
                await _context.AddAsync(book);
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
            var book = await _context.Books.FindAsync(Id);

            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {

            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetById(int Id)
        {
            var book = await _context.Books.FindAsync(Id);

            return book;
        }

        public async Task<bool> Update(Book book)
        {
            var aut = await _context.Books.FindAsync(book.Id);
            if (aut != null)
            {
                aut.Title = book.Title;
                aut.AuthorId = book.AuthorId;
                aut.GerneID = book.GerneID;
                aut.ISBN = book.ISBN;
                aut.YearPublish = book.YearPublish;
                aut.Rating = book.Rating;
                aut.Summary = book.Summary;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }
    }
}
