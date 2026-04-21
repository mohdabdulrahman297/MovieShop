using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Entities;
using MovieShop.Infrastructure.Data;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Repository
{
    public class CastRepository : Repository<Cast>, ICastRepository
    {
        public CastRepository(MovieDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Cast?> GetById(int id)
        {
            return await _dbContext.Casts
                .Include(c => c.MovieCasts)
                    .ThenInclude(mc => mc.Movie)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}