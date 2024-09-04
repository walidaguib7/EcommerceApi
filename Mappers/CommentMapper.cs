using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Comments;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class CommentMapper
    {
        public static Comments ToModel(this CreateCommentDto dto)
        {
            return new Comments
            {
                Content = dto.Content,
                UserId = dto.UserId,
                CreatedAt = dto.CreatedAt
            };
        }

        public static CommentDto ToDto(this Comments comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UserId = comment.UserId,
                username = comment.user.UserName,
                Likes = comment.commentLikes.ToList().Capacity,
                LikeCollection = comment.commentLikes.Select(c => c.ToDto()).ToList()

            };
        }
    }
}