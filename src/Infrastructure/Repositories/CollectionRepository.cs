using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class CollectionRepository : Repository<CollectionEntity>, ICollectionRepository
{
    public CollectionRepository(IDataContext context)
        : base(context)
    {
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