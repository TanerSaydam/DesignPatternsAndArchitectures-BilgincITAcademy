using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Products;

public sealed class Product : Entity
{
    public string Name { get; set; } = default!;
}