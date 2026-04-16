using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.ApplicationCore.Models
{
    public class MovieCardModel
    {
        public int Id { get; set; }
        public string? PosterUrl { get; set; }
        public string? Title { get; set; }
    }
}