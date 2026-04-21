using System.Collections.Generic;

namespace MovieShop.ApplicationCore.Models
{
    public class CastDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePath { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public string Biography { get; set; }

        public List<MovieCardModel> Movies { get; set; } = new List<MovieCardModel>();
    }
}