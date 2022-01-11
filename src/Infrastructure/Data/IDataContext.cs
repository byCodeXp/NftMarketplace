using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public interface IDataContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}