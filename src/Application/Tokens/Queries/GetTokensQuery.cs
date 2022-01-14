using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tokens.Queries;

public class GetTokensQuery : IRequest<ICollection<TokenDto>>, BaseRequest
{
    public int Page { get; set; }
    public int PerPage { get; set; }
}

public class GetTokensHandler : IRequestHandler<GetTokensQuery, ICollection<TokenDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTokensHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private int Offset (int page, int perPage) => page <= 1 ? 0 : page * perPage - perPage;
    
    public async Task<ICollection<TokenDto>> Handle(GetTokensQuery request, CancellationToken cancellationToken)
    {
        int offset = Offset(request.Page, request.PerPage);

        var tokens = _unitOfWork
            .TokenRepository
            .GetTokens()
            .ProjectToType<TokenDto>()
            .Skip(offset)
            .Take(request.PerPage);

        return await tokens.ToListAsync(cancellationToken);
    }
}