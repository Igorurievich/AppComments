using App.Comments.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace App.Comments.Data
{
    public class CommentsContext : DbContext
    {
		public CommentsContext()
		{
		}

		public CommentsContext(DbContextOptions<CommentsContext> options) : base(options)
        {
			
		}

        public DbSet<Comment> Comments { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
			modelBuilder.Entity<Comment>().ToTable("Comment");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			optionsBuilder.EnableSensitiveDataLogging(true);
        }
    }
}
