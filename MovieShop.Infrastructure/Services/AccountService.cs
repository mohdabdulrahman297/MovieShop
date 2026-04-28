using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Models;
using System.Security.Cryptography;
using System.Text;

namespace MovieShop.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserProfileModel> RegisterUser(RegisterModel model)
        {
            // Check if email already exists
            var existing = await _userRepository.GetUserByEmail(model.Email);
            if (existing != null)
                throw new Exception("An account with this email already exists.");

            // Hash password with salt
            var salt = GenerateSalt();
            var hashedPassword = HashPassword(model.Password, salt);

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                HashedPassword = hashedPassword,
                Salt = salt,
                DateOfBirth = model.DateOfBirth
            };

            var created = await _userRepository.Add(user);
            return MapToProfile(created);
        }

        public async Task<UserProfileModel?> LoginUser(LoginModel model)
        {
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user == null) return null;

            // Verify password
            var hashedPassword = HashPassword(model.Password, user.Salt);
            if (hashedPassword != user.HashedPassword) return null;

            return MapToProfile(user);
        }

        // ── Private Helpers ──────────────────────────────────
        private string GenerateSalt()
        {
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        private string HashPassword(string password, string salt)
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(salt));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        private UserProfileModel MapToProfile(User user) => new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            ProfilePictureUrl = user.ProfilePictureUrl,
            IsAdmin = user.IsAdmin
        };
    }
}