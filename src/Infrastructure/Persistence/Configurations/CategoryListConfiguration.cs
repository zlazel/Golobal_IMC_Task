
using Golobal_IMC_Task.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Golobal_IMC_Task.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(t => t.CategoryName)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
