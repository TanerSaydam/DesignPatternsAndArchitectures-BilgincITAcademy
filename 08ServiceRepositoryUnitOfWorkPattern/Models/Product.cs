namespace _08ServiceRepositoryUnitOfWorkPattern.Models;

public sealed class Product
{
    public Product()
    {
        Id = Guid.CreateVersion7(); //.NET 9 
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}
