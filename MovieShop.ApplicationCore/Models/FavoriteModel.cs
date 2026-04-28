namespace MovieShop.ApplicationCore.Models
{
    public class FavoriteModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? PosterUrl { get; set; }
    }
}