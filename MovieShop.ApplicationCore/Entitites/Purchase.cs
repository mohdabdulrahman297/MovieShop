using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.ApplicationCore.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public Guid PurchaseNumber { get; set; } = Guid.NewGuid();
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDateTime { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User User { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
    }
}