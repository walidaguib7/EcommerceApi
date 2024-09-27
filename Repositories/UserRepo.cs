using Ecommerce.Data;
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
        IToken tokenService,
        IVerification _verification,
        ApplicationDBContext _context

        ) : IUser
    {
        private readonly UserManager<User> manager = _manager;
        private readonly SignInManager<User> signInManager = _signin;
        private readonly IValidator<RegisterDto> _RegisterValidator = RegisterValidator;
        private readonly IValidator<LoginDto> _LoginValidator = LoginValidator;
        private readonly IToken _tokenService = tokenService;
        private readonly IVerification verification = _verification;
        private readonly ApplicationDBContext context = _context;



        public async Task<User?> CreateUser(RegisterDto dto)
        {
            var result = _RegisterValidator.Validate(dto);
            if (result.IsValid)
            {
                var user = new User
                {
                    UserName = dto.username,
                    Email = dto.email,
                    role = dto.role
                };
                Console.WriteLine($"{user.role}");

                var model = await manager.CreateAsync(user, dto.password);
                if (model.Succeeded)
                {
                    if (dto.role == Helpers.Role.Admin)
                    {
                        await manager.AddToRoleAsync(user, "admin");
                    }
                    else
                    {
                        await manager.AddToRoleAsync(user, "user");
                    }


                    // send email verification
                    int code = verification.GenerateCode();
                    await verification.SendVerificationEmail(dto.email, "", code);
                    await verification.CreateVerification(new EmailVerification
                    {
                        code = code,
                        userId = user.Id
                    });

                }

                return user;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<User?> DeleteUser(string userId)
        {
            var user = await context.Users.Where(u => u.Id == userId).FirstAsync();
            if (user == null) return null;
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return user;
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
                    return new NewUser { Id = user.Id, token = _tokenService.createToken(user), role = user.role.ToString() };
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

        public Task<User?> UpdateUser()
        {
            throw new NotImplementedException();
        }

        public async Task<bool?> VerifyUser(string userId, int verificationCode)
        {
            var verification = await context.emailVerifications.Where(e => e.code == verificationCode).FirstAsync();

            var user = await context.Users.Where(u => u.Id == userId).FirstAsync();
            if (verification == null || user == null) return null;

            if (verification.code == verificationCode)
            {
                user.EmailConfirmed = true;
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
