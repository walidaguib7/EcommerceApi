using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Blocking;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IBlocking
    {
        public Task<List<BlockedUsers>> GetBlockedUsers(string userId);
        public Task<BlockedUsers?> BlockUser(BlockUserDto dto);
        public Task<BlockedUsers?> UnblockUser(BlockUserDto dto);
    }
}