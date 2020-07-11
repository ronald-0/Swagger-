using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWeb.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id  { get; set; }
        public string  Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string ISBN { get; set; }
        public int YearPublish { get; set; }
        public int Rating { get; set; }
        public string  Summary { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }
}
