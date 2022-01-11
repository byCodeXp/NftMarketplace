using Domain.Entities;

namespace Infrastructure.Repositories;

public interface ICollectionRepository
{
    IQueryable<Collection> GetCollections();
    Task AddCollection(Collection collection);
}