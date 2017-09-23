using App.Comments.Common.Entities;
using Microsoft.EntityFrameworkCore;
using App.Comments.Common.Mapping;

namespace App.Comments.Data
{
    public class CommentsContext : DbContext
    {
        public CommentsContext(DbContextOptions<CommentsContext> options) : base(options)
        {

        }

        DbSet<Comment> Comments { get; set; }
        DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new ApplicationUserMap());
        }
    }
}
