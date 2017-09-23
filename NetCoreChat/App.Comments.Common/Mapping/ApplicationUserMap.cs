using App.Comments.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Comments.Common.Mapping
{
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(k => k.Id);
            builder.Property(c => c.UserName).HasMaxLength(50).IsRequired();
            builder.Property(c => c.PasswordHash).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(50).IsRequired();
            builder.Property(c => c.UserPhoto).HasMaxLength(10000);

            builder.HasMany(typeof(Comment), "Comments");
        }
    }
}
