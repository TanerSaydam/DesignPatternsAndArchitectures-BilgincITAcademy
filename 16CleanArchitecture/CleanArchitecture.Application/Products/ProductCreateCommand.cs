using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Products;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Products;

public sealed record ProductCreateCommand(
    string Name) : IRequest<Result<Guid>>;

internal sealed class ProductCreateCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<ProductCreateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await productRepository.GetAll().AnyAsync(p => p.Name == request.Name, cancellationToken);
        if (isNameExists)
        {
            return Result<Guid>.Failure("Bu ürün adı daha önce kullanılmış");
        }

        Product product = request.Adapt<Product>();

        productRepository.Add(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}