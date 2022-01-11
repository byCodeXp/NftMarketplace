using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class TokenRepository : Repository<Token>, ITokenRepository
{
    public TokenRepository(DataContext context)
        : base(context)
    {
    }
    
    public IQueryable<Token> GetTokens()
    {
        return Get();
    }

    public Task AddToken(Token token)
    {
        return Add(token);
    }
}