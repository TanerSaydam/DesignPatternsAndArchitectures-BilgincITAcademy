namespace CleanArchitecture.Domain.Abstractions;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
}
