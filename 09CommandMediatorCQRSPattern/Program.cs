using _09CommandMediatorCQRSPattern.Dtos;
using _09CommandMediatorCQRSPattern.Models;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MediatorPattern>();
builder.Services.AddMediatR(cfr =>
{
    cfr.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
var app = builder.Build();

app.MapPost("products", async (MediatorPattern mediator, ProductCreateDto request, CancellationToken cancellationToken) =>
{
    await mediator.Handle(request);
});

app.MapPut("products", async (MediatorPattern mediator, ProductUpdateDto request, CancellationToken cancellationToken) =>
{
    await mediator.Handle(request);
});

app.Run();
//Create-Update-Delete - CUD operations
//Read 
//CQRS Pattern - Command - Query Responsibility Segregation

#region CQRS

public sealed class MSSqlDb { }
public sealed class PostgeSqlDb { }
public interface IMediator
{
    void Handle();
}
public interface ICommand : IMediator
{
}

public interface IQuery : IMediator
{
}

public sealed record ProductCreateCommand(string Name) : IRequest;
internal sealed class ProductCreateCommandHandle(MSSqlDb mSSqlDb) : IRequestHandler<ProductCreateCommand>
{
    public Task Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public sealed class ProductUpdateCommand(MSSqlDb mSSqlDb) : ICommand
{
    public void Handle() { }
}

public sealed class ProductDeleteCommand(MSSqlDb mSSqlDb) : ICommand
{
    public void Handle() { }
}

public sealed class ProductGetQuery(PostgeSqlDb postgeSqlDb) : IQuery
{
    public void Handle() { }
}

public sealed class ProductGetAllQuery(PostgeSqlDb postgeSqlDb) : IQuery
{
    public void Handle() { }
}

#endregion

#region Command and Mediator Pattern
public sealed class MediatorPattern
{
    public async Task Handle(object request)
    {
        //var assembly = Assembly.GetExecutingAssembly();
        //var types = assembly.GetTypes();
        if (request.GetType() == typeof(ProductCreateDto)) //reflection
        {
            ProductCreateCommandPattern productCreate = new();
            await productCreate.Handle((ProductCreateDto)request, default);
        }
        else if (request.GetType() == typeof(ProductUpdateDto))
        {
            ProductUpdateCommandPattern productUpdate = new();
            await productUpdate.Handle((ProductUpdateDto)request, default);
        }
    }
}

public class ProductServiceCommandPattern
{
    public async Task CreateCommand(ProductCreateDto request, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public async Task UpdateCommand(ProductUpdateDto request, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public async Task DeleteCommand(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public async Task<Product> GetQuery(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return new Product();
    }

    public async Task<List<Product>> GetAllQuery(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return new List<Product>();
    }
}

public sealed class ProductCreateCommandPattern
{
    public async Task Handle(ProductCreateDto request, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}

public sealed class ProductUpdateCommandPattern
{
    public async Task Handle(ProductUpdateDto request, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}
#endregion