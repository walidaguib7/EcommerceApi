
using Ecommerce.Dtos.User;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Ecommerce.Validations.User;
using FluentValidation;

namespace Ecommerce.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUser,UserRepo>();
            services.AddSingleton<IToken, TokenRepo>();

            services.AddKeyedScoped<IValidator<RegisterDto>, RegisterValidation>("register");
            services.AddKeyedScoped<IValidator<LoginDto>, LoginValidation>("login");
        }
    }
}
