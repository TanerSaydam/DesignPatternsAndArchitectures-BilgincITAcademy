namespace CleanArchitecture.Domain.Abstractions;

public interface IRepository<T>
    where T : class
{
    void Add(T entity);
    IQueryable<T> GetAll();
}
