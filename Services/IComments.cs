using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Comments;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IComments
    {
        public Task<ICollection<Comments>> GetAllComments();
        public Task<Comments?> GetComment(int id);
        public Task<Comments> CreateComment(CreateCommentDto dto);
        public Task<Comments?> UpdateComment(int id, UpdateCommentDto dto);
        public Task<Comments?> DeleteComment(int id);
    }
}