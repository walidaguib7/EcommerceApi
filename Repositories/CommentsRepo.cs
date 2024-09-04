using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Dtos.Comments;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class CommentsRepo
    (
        ApplicationDBContext _context,
        [FromKeyedServices("createComment")] IValidator<CreateCommentDto> _CreateCommentValidator,
        [FromKeyedServices("updateComment")] IValidator<UpdateCommentDto> _UpdateCommentValidator,
        ICache _cacheService
    ) : IComments
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<CreateCommentDto> CreateCommentValidator = _CreateCommentValidator;
        private readonly IValidator<UpdateCommentDto> UpdateCommentValidator = _UpdateCommentValidator;
        private readonly ICache cacheService = _cacheService;
        public async Task<Comments> CreateComment(CreateCommentDto dto)
        {
            var result = CreateCommentValidator.Validate(dto);
            if (result.IsValid)
            {
                var comment = dto.ToModel();
                await context.comments.AddAsync(comment);
                await context.SaveChangesAsync();
                return comment;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<Comments?> DeleteComment(int id)
        {
            var comment = await context.comments.Where(c => c.Id == id).FirstAsync();
            if (comment == null) return null;
            context.comments.Remove(comment);
            await context.SaveChangesAsync();
            return comment;

        }

        public async Task<ICollection<Comments>> GetAllComments()
        {
            string key = $"comments";
            var cachedComments = await cacheService.GetFromCacheAsync<ICollection<Comments>>(key);
            if (!cachedComments.IsNullOrEmpty()) return cachedComments;
            var comments = await context.comments
            .Include(c => c.user)
            .Include(c => c.user.Profile)
            .ToListAsync();
            await cacheService.SetAsync(key, comments);
            return comments;
        }

        public async Task<Comments?> GetComment(int id)
        {
            string key = $"comment_{id}";
            var cachedComment = await cacheService.GetFromCacheAsync<Comments>(key);
            if (cachedComment != null) return cachedComment;
            var comment = await context.comments
            .Include(c => c.user)
            .Where(c => c.Id == id).FirstAsync();
            if (comment == null) return null;
            await cacheService.SetAsync(key, comment);
            return comment;
        }

        public async Task<Comments?> UpdateComment(int id, UpdateCommentDto dto)
        {
            var result = UpdateCommentValidator.Validate(dto);
            if (result.IsValid)
            {
                var comment = await context.comments
             .Where(c => c.Id == id).FirstAsync();
                if (comment == null) return null;
                comment.Content = dto.Content;
                await context.SaveChangesAsync();
                return comment;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}