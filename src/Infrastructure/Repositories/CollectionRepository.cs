using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class CollectionRepository : Repository<Collection>, ICollectionRepository
{
    public CollectionRepository(DataContext context)
        : base(context)
    {
    }

    public IQueryable<Collection> GetCollections()
    {
        return Get();
    }

    public Task AddCollection(Collection collection)
    {
        return Add(collection);
    }
}