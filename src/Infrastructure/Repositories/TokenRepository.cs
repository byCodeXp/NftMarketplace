using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class TokenRepository : Repository<TokenEntity>, ITokenRepository
{
    public TokenRepository(IDataContext context)
        : base(context)
    {
    }
    
    public IQueryable<TokenEntity> GetTokens()
    {
        return Get();
    }

    public Task AddToken(TokenEntity tokenEntity)
    {
        return Add(tokenEntity);
    }
}