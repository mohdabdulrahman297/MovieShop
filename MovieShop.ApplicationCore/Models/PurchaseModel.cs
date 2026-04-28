namespace MovieShop.ApplicationCore.Models
{
    public class PurchaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? PosterUrl { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid PurchaseNumber { get; set; }
        public DateTime PurchaseDateTime { get; set; }
    }
}