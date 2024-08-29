using Ecommerce.Data;
using Ecommerce.Dtos.Blocking;
using Ecommerce.Dtos.Category;
using Ecommerce.Dtos.Followers;
using Ecommerce.Dtos.Media;
using Ecommerce.Dtos.Profile;
using Ecommerce.Dtos.User;
using Ecommerce.Models;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Ecommerce.Validations.Blocking;
using Ecommerce.Validations.Category;
using Ecommerce.Validations.Following;
using Ecommerce.Validations.Media;
using Ecommerce.Validations.Profile;
using Ecommerce.Validations.User;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddScoped<IUser, UserRepo>();
            services.AddSingleton<IToken, TokenRepo>();
            services.AddScoped<ICategory, CategoryRepo>();
            services.AddTransient<IMedia, MediaRepo>();
            services.AddScoped<IMessages, MessagesRepo>();
            services.AddScoped<IFollowing, FollowingRepo>();
            services.AddTransient<ICache, CacheRepo>();
            services.AddScoped<IBlocking, blockingRepo>();
            services.AddScoped<IProfile, ProfilesRepo>();
        }

        public static void AddValidationServices(this IServiceCollection services)
        {
            services.AddKeyedScoped<IValidator<RegisterDto>, RegisterValidation>("register");
            services.AddKeyedScoped<IValidator<LoginDto>, LoginValidation>("login");

            services.AddKeyedScoped<IValidator<CreateCategoryDto>, CategoryValidation>("category");
            services.AddKeyedScoped<IValidator<CreateFile>, MediaValidation>("media");
            services.AddKeyedScoped<IValidator<FollowDto>, FollowingValidation>("following");
            services.AddKeyedScoped<IValidator<BlockUserDto>, BlockingValidation>("blocking");
            services.AddKeyedScoped<IValidator<CreateProfileDto>, CreateProfileValidation>("createProfile");
            services.AddKeyedScoped<IValidator<UpdateProfileDto>, UpdateProfileValidation>("UpdateProfile");
        }

        public static void AddCustomAuth(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SignInKey"]))


                };
            });
        }

        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 12;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDBContext>();
        }

        public static void AddDB(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void AddRedisDB(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddStackExchangeRedisCache(opt =>
            {
                string RedisConnectionString = builder.Configuration["Redis:ConnectionString"];
                opt.Configuration = RedisConnectionString;
                opt.InstanceName = "instance";
            });



        }
    }
}
