using Domain.Entities;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Collections.Queries;

public class GetCollectionsQuery : IRequest<CollectionsVm>, BaseRequest
{
    public int Page { get; set; }
    public int PerPage { get; set; }
}

public class GetCollectionsHandler : IRequestHandler<GetCollectionsQuery, CollectionsVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCollectionsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CollectionsVm> Handle(GetCollectionsQuery query, CancellationToken cancellationToken)
    {
        IQueryable<CollectionEntity> collections = _unitOfWork
            .CollectionRepository
            .GetCollections();

        int totalCount = await collections.CountAsync(cancellationToken);
        
        int offset = CalculateOffset.Offset(query.Page, query.PerPage);
        
        List<CollectionDto> selectedCollections = await collections
            .Skip(offset)
            .Take(query.PerPage)
            .ProjectToType<CollectionDto>()
            .ToListAsync(cancellationToken);

        return new CollectionsVm
        {
            TotalCount = totalCount,
            Collections = selectedCollections
        };
    }
}