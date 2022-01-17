using System.Linq.Expressions;
using Domain.Entities.Base;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDataContext _context;

    public UnitOfWork(IDataContext context)
    {
        _context = context;
        TokenRepository = new TokenRepository(context);
        CollectionRepository = new CollectionRepository(context);
    }

    public ITokenRepository TokenRepository { get; }
    public ICollectionRepository CollectionRepository { get; }

    public void Include<TEntity>(TEntity entity, Expression<Func<TEntity, IEnumerable<Entity>>> predicate) where TEntity : class
    {
        _context.Entry(entity).Collection(predicate).Load();
    }
    
    public async Task<int> Completed(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}