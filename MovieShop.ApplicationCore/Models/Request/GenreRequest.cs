using System.ComponentModel.DataAnnotations;

namespace MovieShop.ApplicationCore.Models.Request
{
    public class GenreRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }
    }
}