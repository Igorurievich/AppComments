using App.Comments.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Comments.Common.Mapping
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(k => k.Id);
            builder.Property(c => c.Title).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Description).HasColumnType("varchar").HasMaxLength(300).IsRequired();

            builder.HasOne<ApplicationUser>(u => u.Autor).WithMany(c => c.Comments);
        }
    }
}
