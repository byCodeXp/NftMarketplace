using Domain.Exceptions;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Features.Tokens.Queries;

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

    public async Task<TokensVm> Handle(GetCollectionTokensQuery request, CancellationToken cancellationToken)
    {
        var collection = await _unitOfWork.CollectionRepository.FindCollection(request.CollectionId, cancellationToken);

        if (collection is null)
        {
            throw new BadRequestException("Collection does not exists");
        }
        
        _unitOfWork.Include(collection, entity => entity.Tokens);
        var tokens = collection.Tokens.Adapt<ICollection<TokenDto>>();
        
        int totalCount = tokens.Count;
        
        int offset = CalculateOffset.Offset(request.Page, request.PerPage);
        var selectedTokens = tokens.Skip(offset).Take(request.PerPage).ToList();
        
        return new TokensVm
        {
            Tokens = selectedTokens,
            TotalCount = totalCount
        };
    }
}