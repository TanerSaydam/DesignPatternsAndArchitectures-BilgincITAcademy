using _09CommandMediatorCQRSPattern.Dtos;
using _09CommandMediatorCQRSPattern.Models;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.MapPost("product", async (Mediator mediator, ProductCreateDto request, CancellationToken cancellationToken) =>
{
    await mediator.Handle("product-create", request);
});

app.MapPut("product", async (Mediator mediator, ProductUpdateDto request, CancellationToken cancellationToken) =>
{
    await mediator.Handle("product-update", request);
});

app.Run();

public sealed class Mediator
{
    public async Task Handle(string type, object request)
    {
        switch (type)
        {
            case "product-create":
                ProductCreate productCreate = new();
                await productCreate.Handle((ProductCreateDto)request, default);
                break;
            case "product-update":
                ProductUpdate productUpdate = new();
                await productUpdate.Handle((ProductUpdateDto)request, default);
                break;
            default:
                break;
        }
    }
}

public class ProductService
{
    public async Task Create(ProductCreateDto request, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public async Task Update(ProductUpdateDto request, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public async Task<Product> Get(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return new Product();
    }

    public async Task<List<Product>> GetAll(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return new List<Product>();
    }
}

public sealed class ProductCreate
{
    public async Task Handle(ProductCreateDto request, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}

public sealed class ProductUpdate
{
    public async Task Handle(ProductUpdateDto request, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}