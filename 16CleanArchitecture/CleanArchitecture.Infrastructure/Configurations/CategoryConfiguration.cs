using CleanArchitecture.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(p => p.Name).HasConversion(name => name.Value, value => new Name(value));
        //builder.OwnsOne(p => p.Name);
        //builder.Property(p => p.Name).HasColumnType("nvarchar(100)");
        //builder.HasIndex(i => i.Name).IsUnique(true);
    }
}