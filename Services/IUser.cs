using Ecommerce.Dtos.User;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IUser
    {
        public Task<User?> CreateUser(RegisterDto dto);
        public Task<NewUser?> login(LoginDto dto);
        public Task<bool?> VerifyUser(string userId, int verificationCode);
        public Task<User?> UpdateUser();
        public Task<User?> DeleteUser(string userId);
    }
}
