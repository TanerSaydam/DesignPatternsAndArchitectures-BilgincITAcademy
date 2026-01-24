using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Infrastructure.Context;

namespace CleanArchitecture.Infrastructure.Repositories;

internal class Repository<T>(ApplicationDbContext dbContext) : IRepository<T>
    where T : class
{
    public void Add(T entity)
    {
        dbContext.Set<T>().Add(entity);
    }

    public IQueryable<T> GetAll()
    {
        return dbContext.Set<T>().AsQueryable();
    }
}