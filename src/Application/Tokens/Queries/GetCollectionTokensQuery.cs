using Application.Collections;
using Application.Exceptions;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tokens.Queries;

public record GetCollectionTokensQuery : IRequest<TokensVm>, BaseRequest
{
    public Guid CollectionId { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }
}

public class GetCollectionTokensHandler : IRequestHandler<GetCollectionTokensQuery, TokensVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCollectionTokensHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private int Offset (int page, int perPage) => page <= 1 ? 0 : page * perPage - perPage;
    
    public async Task<TokensVm> Handle(GetCollectionTokensQuery request, CancellationToken cancellationToken)
    {
        var collection = await _unitOfWork.CollectionRepository.FindCollection(request.CollectionId, cancellationToken);

        if (collection is null)
        {
            throw new BadRequestException("Collection does not exists");
        }

        var tokens = _unitOfWork
            .TokenRepository.GetTokens()
            .Where(entity => entity.Collection.Id == collection.Id)
            .ProjectToType<TokenDto>();

        int totalCount = tokens.Count();
        
        
        int offset = Offset(request.Page, request.PerPage);
        
        var selectedTokens = await tokens.Skip(offset).Take(request.PerPage).ToListAsync(cancellationToken);
        
        return new TokensVm
        {
            Tokens = selectedTokens,
            TotalCount = totalCount
        };
    }
}