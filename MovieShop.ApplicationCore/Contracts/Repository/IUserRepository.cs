using MovieShop.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.ApplicationCore.Contracts.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
    }
}
