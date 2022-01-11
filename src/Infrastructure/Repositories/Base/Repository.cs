using Domain.Entities.Base;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Base;

public abstract class Repository<TEntity> where TEntity : Entity
{
    private readonly IDataContext _context;

    protected Repository(IDataContext context)
    {
        _context = context;
    }

    protected IQueryable<TEntity> Get()
    {
        return _context.Set<TEntity>();
    }

    protected async Task Add(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }
}