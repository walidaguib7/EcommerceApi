using Ecommerce.Data;
using Ecommerce.Dtos.ProuctFiles;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FakeItEasy;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class ProductFilesRepo(
        ApplicationDBContext _context,
        [FromKeyedServices("productFile")] IValidator<CreateProductFile> _validator,
        ICache _cacheService
        ) : IProductFiles
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<CreateProductFile> validator = _validator;
        private readonly ICache cacheService = _cacheService;
        public async Task<ProductFiles?> CreateProductFiles(CreateProductFile productFile, string userId)
        {
            var user = await context.Users.FindAsync(userId);
            if (user == null) return null;
            if (user.role == Helpers.Role.Admin)
            {
                var result = validator.Validate(productFile);
                if (result.IsValid)
                {
                    var model = productFile.ToModel();
                    await context.productFiles.AddAsync(model);
                    await context.SaveChangesAsync();
                    return model;
                }
                else
                {
                    throw new ValidationException(result.Errors);
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<IEnumerable<ProductFiles>> GetProductFiles(int ProductId)
        {
            var key = $"pf_{ProductId}";
            var CachedProductFiles = await cacheService.GetFromCacheAsync<IEnumerable<ProductFiles>>(key);
            if (!CachedProductFiles.IsNullOrEmpty()) return CachedProductFiles;
            var productFiles = await context.productFiles
            .Include(pf => pf.Product)
            .Include(pf => pf.file)
            .Where(p => p.ProductId == ProductId).ToListAsync();
            await cacheService.SetAsync(key, productFiles);
            return productFiles;
        }
    }
}