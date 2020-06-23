using System;
using BookAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Data
{
    public class BookApiDataContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public BookApiDataContext(DbContextOptions<BookApiDataContext> options) : base(options)
        {
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Readers> Readers { get; set; }
        public virtual DbSet<User> BookUsers { get; set; }
    }
}

