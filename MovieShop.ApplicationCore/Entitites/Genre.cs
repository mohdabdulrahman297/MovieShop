using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieShop.ApplicationCore.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;  

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>(); 
    }
}
