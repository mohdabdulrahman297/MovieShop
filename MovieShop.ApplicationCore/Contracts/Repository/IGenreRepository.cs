using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Helper;
using MovieShop.ApplicationCore.Contracts.Repository;
using System.Threading.Tasks;

namespace MovieShop.ApplicationCore.Contracts.Repository
{
    public interface IGenreRepository : IRepository<Genre>
    {
        // add more custom methods
        Task<Page<Genre>> GetGenreByPagination(int pageNumber = 1, int pageSize = 3);
    }
}