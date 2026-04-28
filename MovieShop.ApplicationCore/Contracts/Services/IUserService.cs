using MovieShop.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.ApplicationCore.Contracts.Services
{
    public interface IUserService
    {
        Task<UserProfileModel> RegisterUser(RegisterModel registerModel);
        Task<UserProfileModel?> LoginUser(LoginModel loginModel);
    }
}
