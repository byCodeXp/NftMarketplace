using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CollectionRepository : Repository<CollectionEntity>, ICollectionRepository
{
    public CollectionRepository(IDataContext context)
        : base(context)
    {
    }

    public Task<CollectionEntity> FindCollection(Guid id, CancellationToken cancellationToken)
    {
        return Get().FirstOrDefaultAsync(collection => collection.Id == id, cancellationToken);
    }

    public IQueryable<CollectionEntity> GetCollections()
    {
        return Get();
    }

    public Task AddCollection(CollectionEntity collectionEntity)
    {
        return Add(collectionEntity);
    }
}