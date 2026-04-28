using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.ApplicationCore.Entities
{
    public class Review
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public decimal Rating { get; set; }   // 1.0 – 10.0
        public string ReviewText { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User User { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
    }
}
