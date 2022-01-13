using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tokens.Queries;

public class GetTokensQuery : IRequest<ICollection<TokenDto>>, BaseRequest
{
    
}

public class GetTokensHandler : IRequestHandler<GetTokensQuery, ICollection<TokenDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTokensHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ICollection<TokenDto>> Handle(GetTokensQuery request, CancellationToken cancellationToken)
    {
        var tokens = _unitOfWork
            .TokenRepository
            .GetTokens()
            .ProjectToType<TokenDto>();

        return await tokens.ToListAsync(cancellationToken);
    }
}