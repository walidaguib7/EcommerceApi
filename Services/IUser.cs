using Ecommerce.Dtos.User;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IUser
    {
        public Task<User?> CreateUser(RegisterDto dto);
        public Task<NewUser?> login(LoginDto dto);
    }
}
