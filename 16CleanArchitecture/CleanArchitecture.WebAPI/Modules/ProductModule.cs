using Carter;
using CleanArchitecture.Application.Products;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Products;
using MediatR;

namespace CleanArchitecture.WebAPI.Modules;

public sealed class ProductModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("/products").WithTags("Products");

        app.MapGet(string.Empty, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var res = await sender.Send(new ProductGetAllQuery(), cancellationToken);
            return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
        })
            .Produces<Result<List<Product>>>();

    }
}
