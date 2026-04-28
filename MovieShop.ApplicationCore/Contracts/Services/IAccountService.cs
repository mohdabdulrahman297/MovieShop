using MovieShop.ApplicationCore.Models;

namespace MovieShop.ApplicationCore.Contracts.Services
{
    public interface IAccountService
    {
        Task<UserProfileModel> RegisterUser(RegisterModel registerModel);
        Task<UserProfileModel?> LoginUser(LoginModel loginModel);
    }
}