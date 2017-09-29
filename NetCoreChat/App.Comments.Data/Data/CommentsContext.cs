using App.Comments.Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace App.Comments.Data
{
    public class CommentsContext : IdentityDbContext<ApplicationUser>
    {
        public CommentsContext(DbContextOptions<CommentsContext> options) : base(options)
        {

        }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }
    }
}
