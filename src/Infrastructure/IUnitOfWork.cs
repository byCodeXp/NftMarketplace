using Infrastructure.Repositories;

namespace Infrastructure;

public interface IUnitOfWork
{
    public ITokenRepository TokenRepository { get; }
    public ICollectionRepository CollectionRepository { get; }
    
    Task<int> Completed(CancellationToken cancellationToken);
}