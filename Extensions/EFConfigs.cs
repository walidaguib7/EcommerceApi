using Ecommerce.Helpers;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Extensions
{
    public static class EFConfigs
    {
        public static void ConfigEnums(this ModelBuilder builder)
        {
            builder.Entity<Payments>()
                .Property(p => p.Status)
                .HasConversion
                (
                  p => p.ToString(),
                  p => Enum.Parse<StatusTypes>(p)
                );

            ;
        }
    
        public static void ConfigRelations(this ModelBuilder builder)
        {

            //Comment Likes config
            builder.Entity<CommentLikes>(c => c.HasKey(c => new { c.UserId, c.CommentId }));

            builder.Entity<CommentLikes>()
                .HasOne(c => c.user)
                .WithMany(c => c.commentLikes)
                .HasForeignKey(c => c.UserId);
            builder.Entity<CommentLikes>()
                .HasOne(c => c.comment)
                .WithMany(c => c.commentLikes)
                .HasForeignKey(c => c.CommentId);

            // Product Orders Config 
            builder.Entity<Order_Product>(op => op.HasKey(op => new { op.productId, op.orderId }));

            builder.Entity<Order_Product>()
                .HasOne(op => op.order)
                .WithMany(op => op.order_Products)
                .HasForeignKey(op => op.orderId);
            builder.Entity<Order_Product>()
                .HasOne(op => op.product)
                .WithMany(op => op.order_Products)
                .HasForeignKey(op => op.productId);

            //Product Files Config
            builder.Entity<ProductFiles>(pf => pf.HasKey(pf => new { pf.fileId, pf.ProductId }));

            builder.Entity<ProductFiles>()
                .HasOne(pf => pf.Product)
                .WithMany(pf => pf.productFiles)
                .HasForeignKey(pf => pf.ProductId);
            builder.Entity<ProductFiles>()
                .HasOne(pf => pf.file)
                .WithMany(pf => pf.productFiles)
                .HasForeignKey(pf => pf.fileId);

            //Followers config
            builder.Entity<Follower>(f => f.HasKey(f => new { f.UserId, f.FollowerId }));

            builder.Entity<User>()
                .HasMany(f => f.followers)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId);

            //Blocked users config
            builder.Entity<BlockedUsers>(bu => bu.HasKey(bu => new { bu.userId, bu.blockedUserId }));
            builder.Entity<User>()
                .HasMany(f => f.blockedUsers)
                .WithOne(f => f.user)
                .HasForeignKey(f => f.userId);

            builder.Entity<Following>(f => f.HasKey(f => new { f.followerId, f.followingId }));
            builder.Entity<User>()
                .HasMany(f => f.followings)
                .WithOne(f => f.follower)
                .HasForeignKey(f => f.followerId);
        }
    }
}
