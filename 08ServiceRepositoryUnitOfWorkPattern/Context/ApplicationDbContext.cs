using _08ServiceRepositoryUnitOfWorkPattern.Models;
using Microsoft.EntityFrameworkCore;

namespace _08ServiceRepositoryUnitOfWorkPattern.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
