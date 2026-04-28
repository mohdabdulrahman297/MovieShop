using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Models;

namespace MovieShop.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public Task<UserProfileModel> RegisterUser(RegisterModel registerModel)
            => throw new NotImplementedException();

        public Task<UserProfileModel?> LoginUser(LoginModel loginModel)
            => throw new NotImplementedException();
    }
}