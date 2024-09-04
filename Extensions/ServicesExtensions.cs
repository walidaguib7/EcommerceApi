using System.Threading.RateLimiting;
using Ecommerce.Data;
using Ecommerce.Dtos.Blocking;
using Ecommerce.Dtos.Category;
using Ecommerce.Dtos.Comments;
using Ecommerce.Dtos.Comments.CommentLikes;
using Ecommerce.Dtos.Followers;
using Ecommerce.Dtos.MediaDtos;
using Ecommerce.Dtos.Products;
using Ecommerce.Dtos.Profile;
using Ecommerce.Dtos.ProuctFiles;
using Ecommerce.Dtos.User;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Ecommerce.Validations.Blocking;
using Ecommerce.Validations.Category;
using Ecommerce.Validations.Comments;
using Ecommerce.Validations.Comments.CommentLikes;
using Ecommerce.Validations.Following;
using Ecommerce.Validations.Media;
using Ecommerce.Validations.ProductFiles;
using Ecommerce.Validations.Products;
using Ecommerce.Validations.Profile;
using Ecommerce.Validations.User;
using FluentValidation;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;


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
            services.AddTransient<IProduct, ProductsRepo>();
            services.AddScoped<IProductFiles, ProductFilesRepo>();
            services.AddScoped<IComments, CommentsRepo>();
            services.AddSingleton<ICommentLikes, CommentlikesRepo>();
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

            services.AddKeyedScoped<IValidator<CreateProductDto>, CreateProductValidation>("createProduct");
            services.AddKeyedScoped<IValidator<UpdateProductDto>, UpdateProductValidation>("updateProduct");

            services.AddKeyedScoped<IValidator<CreateProductFile>, ProductFilesValidation>("productFile");

            services.AddKeyedScoped<IValidator<CreateCommentDto>, CreateCommentValiation>("createComment");
            services.AddKeyedScoped<IValidator<UpdateCommentDto>, UpdateCommentValidation>("updateComment");

            services.AddKeyedSingleton<IValidator<CreateCommentLike>, CommentLikesValidation>("commentLike");
        }

        public static void AddDB(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void AddRateLimit(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddFixedWindowLimiter("fixed", _ =>
                {
                    _.Window = TimeSpan.FromSeconds(10);
                    _.PermitLimit = 5;
                    _.QueueLimit = 2;
                    _.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
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
