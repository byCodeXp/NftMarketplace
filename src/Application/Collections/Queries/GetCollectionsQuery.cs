using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Collections.Queries;

public class GetCollectionsQuery : IRequest<ICollection<CollectionDto>>, BaseRequest
{
    public int Page { get; set; }
    public int PerPage { get; set; }
}

public class GetCollectionsHandler : IRequestHandler<GetCollectionsQuery, ICollection<CollectionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCollectionsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private int Offset (int page, int perPage) => page <= 1 ? 0 : page * perPage - perPage;
    
    public async Task<ICollection<CollectionDto>> Handle(GetCollectionsQuery query, CancellationToken cancellationToken)
    {
        int offset = Offset(query.Page, query.PerPage);

        var collections = _unitOfWork
            .CollectionRepository
            .GetCollections()
            .ProjectToType<CollectionDto>()
            .Skip(offset)
            .Take(query.PerPage);
        
        return await collections.ToListAsync(cancellationToken);
    }
}