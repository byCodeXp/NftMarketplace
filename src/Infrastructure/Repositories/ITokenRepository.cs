using Domain.Entities;

namespace Infrastructure.Repositories;

public interface ITokenRepository
{
    IQueryable<TokenEntity>  GetTokens();
    Task AddToken(TokenEntity tokenEntity);
}