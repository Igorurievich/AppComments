using App.Comments.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Comments.Data
{
    public class CommentsContext : DbContext
    {
        public CommentsContext(DbContextOptions<CommentsContext> options) : base(options)
        {
             
        }

        DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
