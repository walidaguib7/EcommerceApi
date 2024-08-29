using Ecommerce.Dtos.User;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class UserRepo(
        UserManager<User> _manager,
        SignInManager<User> _signin,
        [FromKeyedServices("register")] IValidator<RegisterDto> RegisterValidator,
        [FromKeyedServices("login")] IValidator<LoginDto> LoginValidator,
        IToken tokenService
        ) : IUser
    {
        private readonly UserManager<User> manager = _manager;
        private readonly SignInManager<User> signInManager = _signin;
        private readonly IValidator<RegisterDto> _RegisterValidator = RegisterValidator;
        private readonly IValidator<LoginDto> _LoginValidator = LoginValidator;
        private readonly IToken _tokenService = tokenService;
        public async Task<User?> CreateUser(RegisterDto dto)
        {
            var result = _RegisterValidator.Validate(dto);
            if (result.IsValid)
            {
                var user = dto.toUser();

                var model = await manager.CreateAsync(user, dto.password);
                if (model.Succeeded)
                {
                    if (dto.isAdmin == true)
                    {
                        await manager.AddToRoleAsync(user, "admin");
                    }
                    else
                    {
                        await manager.AddToRoleAsync(user, "user");
                    }
                }

                return user;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<NewUser?> login(LoginDto dto)
        {
            var result = _LoginValidator.Validate(dto);
            if (result.IsValid)
            {
                var user = await manager.Users

                    .FirstOrDefaultAsync(u => u.UserName == dto.username);
                if (user == null) return null;
                var status = await signInManager.CheckPasswordSignInAsync(user, dto.password, false);
                if (status.Succeeded)
                {
                    return new NewUser { Id = user.Id, token = _tokenService.createToken(user) };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
