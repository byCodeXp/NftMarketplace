using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Collections.Queries;

public class GetCollectionsQuery : IRequest<ICollection<CollectionDto>>, BaseRequest
{
    
}

public class GetCollectionsHandler : IRequestHandler<GetCollectionsQuery, ICollection<CollectionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCollectionsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ICollection<CollectionDto>> Handle(GetCollectionsQuery query, CancellationToken cancellationToken)
    {
        var collections = _unitOfWork
            .CollectionRepository
            .GetCollections()
            .ProjectToType<CollectionDto>();
        
        return await collections.ToListAsync(cancellationToken);
    }
}