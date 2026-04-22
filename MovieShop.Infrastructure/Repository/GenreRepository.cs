using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Helper;
using MovieShop.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Page<Genre>> GetGenreByPagination(int pageNumber = 1, int pageSize = 3)
        {
            var all = await GetAll(); 

            Page<Genre> page = new Page<Genre>();
            page.PageNumber = pageNumber;
            page.TotalRecords = all.Count();                                       
            page.Data = all.Skip((pageNumber - 1) * pageSize).Take(pageSize);    
            return page;
        }
    }
}