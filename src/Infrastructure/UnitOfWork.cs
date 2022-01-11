using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public UnitOfWork(DataContext context)
    {
        _context = context;
        
        TokenRepository = new TokenRepository(context);
        CollectionRepository = new CollectionRepository(context);
    }

    public ITokenRepository TokenRepository { get; }
    public ICollectionRepository CollectionRepository { get; }

    public async Task<int> Completed(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}