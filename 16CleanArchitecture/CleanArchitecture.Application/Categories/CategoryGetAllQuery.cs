using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Categories;

public sealed class CategoryGetAllQuery : IRequest<Result<List<CategoryGetAllQueryResponse>>>;

public sealed record CategoryGetAllQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

internal sealed class CategoryGetAllQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<CategoryGetAllQuery, Result<List<CategoryGetAllQueryResponse>>>
{
    public async Task<Result<List<CategoryGetAllQueryResponse>>> Handle(CategoryGetAllQuery request, CancellationToken cancellationToken)
    {
        var res = await categoryRepository
            .GetAll()
            .OrderBy(p => p.Name)
            .Select(s => new CategoryGetAllQueryResponse
            {
                Id = s.Id,
                Name = s.Name.Value,
            })
            .ToListAsync(cancellationToken);
        return res;
    }
}