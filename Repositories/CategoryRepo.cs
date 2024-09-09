﻿using Ecommerce.Data;
using Ecommerce.Dtos.Category;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class CategoryRepo
        (
          ApplicationDBContext _context,
          [FromKeyedServices("category")] IValidator<CreateCategoryDto> _validator,
          ICache _cacheService
        )
        : ICategory
    {

        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<CreateCategoryDto> validator = _validator;
        private readonly ICache cacheService = _cacheService;
        public async Task<Category> CreateCategory(CreateCategoryDto dto)
        {
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                var category = dto.ToCategory();
                await context.categories.AddAsync(category);
                await context.SaveChangesAsync();
                return category;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<Category?> DeleteCategory(int id)
        {
            var category = await context.categories.FindAsync(id);
            if (category == null) return null;
            context.categories.Remove(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var key = $"categories";
            var CachedCategories = await cacheService.GetFromCacheAsync<IEnumerable<Category>>(key);
            if (!CachedCategories.IsNullOrEmpty())
            {
                return CachedCategories;
            }
            var categories = await context.categories.ToListAsync();
            await cacheService.SetAsync(key, categories, TimeSpan.FromSeconds(30));
            return categories;
        }

        public async Task<Category?> GetCategoryAsync(int id)
        {
            var category = await context.categories.FindAsync(id);
            if (category == null) return null;
            return category;
        }

        public async Task<Category?> UpdateCategory(int id, CreateCategoryDto dto)
        {
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                var category = await context.categories.FindAsync(id);
                if (category == null) return null;
                category.Name = dto.Name;
                await context.SaveChangesAsync();
                return category;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }

        }
    }
}
