using Carter;
using CleanArchitecture.Application.Categories;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Categories;
using MediatR;

namespace CleanArchitecture.WebAPI.Modules;

public sealed class CategoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group
            .MapGroup("/categories")
            .WithTags("Categories")
            .RequireRateLimiting("fixed");

        app.MapGet(string.Empty, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var res = await sender.Send(new CategoryGetAllQuery(), cancellationToken);
            return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
        })
            .Produces<Result<List<Category>>>();

        app.MapPost(string.Empty, async (CategoryCreateCommand request, ISender sender, CancellationToken cancellationToken) =>
        {
            var res = await sender.Send(request, cancellationToken);
            return res.IsSuccessful ? Results.Ok(res) : Results.BadRequest(res);
        })
            .Produces<Result<Guid>>();
    }
}
