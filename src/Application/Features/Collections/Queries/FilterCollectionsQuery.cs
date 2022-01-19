using Application.Types;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Collections.Queries;

public record FilterCollectionsQuery : IRequest<CollectionsVm>, BaseRequest
{
    public string Search { get; set; }
    public SortCollections Sort { get; set; }
    
    /// <summary>
    /// If value true - will be sorting ascending. Default sorting by descending
    /// </summary>
    public bool Reverse { get; set; }
    
    /// <summary>
    /// Page number what you need
    /// </summary>
    public int Page { get; set; }
    
    /// <summary>
    /// Count collections on page what you need
    /// </summary>
    public int PerPage { get; set; }
}

public class FilterCollectionsHandler : IRequestHandler<FilterCollectionsQuery, CollectionsVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public FilterCollectionsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CollectionsVm> Handle(FilterCollectionsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.CollectionRepository.GetCollections();
        
        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(entity => entity.Name.Contains(request.Search));
        }

        switch (request.Reverse)
        {
            case true:
            {
                query = query.OrderBy(entity => request.Sort);
                break;
            }
            case false:
            {
                query = query.OrderByDescending(entity => request.Sort);
                break;
            }
        }

        int totalCount = await query.CountAsync(cancellationToken);

        int offset = CalculateOffset.Offset(request.Page, request.PerPage);
        
        var collections = await query
            .Skip(offset)
            .Take(request.PerPage)
            .ProjectToType<CollectionDto>()
            .ToListAsync(cancellationToken);
        
        return new CollectionsVm
        {
            TotalCount = totalCount,
            Collections = collections
        };
    }
}