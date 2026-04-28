namespace MovieShop.ApplicationCore.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsLocked { get; set; }
        public bool IsAdmin { get; set; }

        // Navigation Properties
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}