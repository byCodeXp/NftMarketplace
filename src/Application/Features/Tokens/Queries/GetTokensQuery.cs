using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tokens.Queries;

public class GetTokensQuery : IRequest<TokensVm>, BaseRequest
{
    public int Page { get; set; }
    public int PerPage { get; set; }
}

public class GetTokensHandler : IRequestHandler<GetTokensQuery, TokensVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTokensHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TokensVm> Handle(GetTokensQuery request, CancellationToken cancellationToken)
    {
        IQueryable<TokenDto> tokens = _unitOfWork
            .TokenRepository
            .GetTokens()
            .ProjectToType<TokenDto>();

        int totalCount = await tokens.CountAsync(cancellationToken);
        
        int offset = CalculateOffset.Offset(request.Page, request.PerPage);
        
        List<TokenDto> selectedTokens = await tokens
            .Skip(offset)
            .Take(request.PerPage)
            .ToListAsync(cancellationToken);
        
        return new TokensVm
        {
            TotalCount = totalCount,
            Tokens = selectedTokens
        };
    }
}