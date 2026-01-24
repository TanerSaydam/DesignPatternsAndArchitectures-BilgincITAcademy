using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Categories;
using CleanArchitecture.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Context;

internal sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
