using Domain.Entities;

namespace Infrastructure.Repositories;

public interface ICollectionRepository
{
    IQueryable<CollectionEntity> GetCollections();
    Task AddCollection(CollectionEntity collectionEntity);
}