using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Products;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Options;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure;

public static class InfrastructureRegistrar
{
    public static void AddInfrastructure(this IServiceCollection services, IOptions<ConnectionStringsOptions> options)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlServer(options.Value.SqlServer);
        });
    }
}
