using Domain.Entities;

namespace Infrastructure.Repositories;

public interface ICollectionRepository
{
    Task<CollectionEntity> FindCollection(Guid id, CancellationToken cancellationToken);
    IQueryable<CollectionEntity> GetCollections();
    Task AddCollection(CollectionEntity collectionEntity);
}