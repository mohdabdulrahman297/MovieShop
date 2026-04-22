using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Helper;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetHighestGrossingMovies()
        {
            return await _dbContext.Movies
                .OrderByDescending(m => m.Revenue)
                .ToListAsync();
        }

        public async Task<Movie?> GetMovieById(int id)
        {
            return await _dbContext.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieCasts)
                    .ThenInclude(mc => mc.Cast)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Page<Movie>> GetMoviesByGenre(int genreId, int pageNumber = 1, int pageSize = 30)
        {
            // Filter movies belonging to the genre via join table
            var query = _dbContext.Movies
                .Where(m => m.MovieGenres.Any(mg => mg.GenreId == genreId));

            var page = new Page<Movie>
            {
                PageNumber = pageNumber,
                TotalRecords = await query.CountAsync(),       
                Data = await query
                    .OrderByDescending(m => m.Revenue)          
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync()                             
            };

            return page;
        }
    }
}