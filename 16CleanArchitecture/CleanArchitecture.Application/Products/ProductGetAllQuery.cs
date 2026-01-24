using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Products;

public sealed record ProductGetAllQuery() : IRequest<Result<List<ProductGetAllQueryResponse>>>;

public sealed record ProductGetAllQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

internal sealed class ProductGetAllQueryHandler(
    IProductRepository productRepository) : IRequestHandler<ProductGetAllQuery, Result<List<ProductGetAllQueryResponse>>>
{
    public async Task<Result<List<ProductGetAllQueryResponse>>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
    {
        var res = await productRepository
            .GetAll()
            .OrderBy(p => p.Name)
            .Select(s => new ProductGetAllQueryResponse
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync(cancellationToken);

        return res;
    }
}