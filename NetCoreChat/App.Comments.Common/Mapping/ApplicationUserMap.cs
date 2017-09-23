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
            builder.Property(c => c.UserName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(c => c.PasswordHash).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Email).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(c => c.UserPhoto).HasColumnType("varbinary").HasMaxLength(10000);

            builder.HasMany(typeof(Comment), "Comments");
        }
    }
}
