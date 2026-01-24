using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Categories;

public sealed class Category : Entity
{
    private Category()
    {
    }
    public Name Name { get; private set; } = default!;

    public static Category Create(Name name)
    {
        return new Category() { Name = name };
    }

    public void Update(Name name)
    {
        Name = name;
    }
}

public sealed record Name
{
    public readonly string Value;
    public Name(string _value)
    {
        //_bu aldığım _value gerçekten bir kategori adımı 
        Value = _value;
    }
}