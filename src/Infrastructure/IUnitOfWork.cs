using System.Linq.Expressions;
using Domain.Entities.Base;
using Infrastructure.Repositories;

namespace Infrastructure;

public interface IUnitOfWork
{
    public ITokenRepository TokenRepository { get; }
    public ICollectionRepository CollectionRepository { get; }

    void Include<TEntity>(TEntity entity, Expression<Func<TEntity, IEnumerable<Entity>>> predicate) where TEntity : class;
    Task<int> Completed(CancellationToken cancellationToken);
}