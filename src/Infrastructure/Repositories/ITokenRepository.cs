using Domain.Entities;

namespace Infrastructure.Repositories;

public interface ITokenRepository
{
    IQueryable<Token>  GetTokens();
    Task AddToken(Token token);
}