using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.ApplicationCore.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
    }
}