using Ecommerce.Data;
using Ecommerce.Dtos.ProuctFiles;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FakeItEasy;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class ProductFilesRepo(
        ApplicationDBContext _context,
        IValidator<CreateProductFile> _validator
        ) : IProductFiles
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<CreateProductFile> validator = _validator;
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
            return await context.productFiles.Where(p => p.ProductId == ProductId).ToListAsync();
        }
    }
}