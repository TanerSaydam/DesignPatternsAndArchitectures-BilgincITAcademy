using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scrutor;

namespace CleanArchitecture.Infrastructure;

public static class InfrastructureRegistrar
{
    public static void AddInfrastructure(this IServiceCollection services, IOptions<ConnectionStringsOptions> options)
    {
        services.Scan(x => x
        .FromAssemblies(typeof(InfrastructureRegistrar).Assembly)
        .AddClasses(publicOnly: false)
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlServer(options.Value.SqlServer);
        });
    }
}
