
using System.Collections.Generic;


namespace MovieShop.ApplicationCore.Helper
{
    public class Page<T> where T : class
    {
        public int PageNumber { get; set; }
        public int TotalRecords { get; set; }

        public IEnumerable<T> Data { get; set; } = new List<T>();
    }
}
