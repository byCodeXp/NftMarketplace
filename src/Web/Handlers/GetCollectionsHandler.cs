using Infrastructure;
using Mapster;
using MediatR;
using Web.Endpoints.Requests;
using Web.Models;

namespace Web.Handlers;

public class GetCollectionsHandler: IRequestHandler<GetCollectionsRequest, ICollection<CollectionVm>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCollectionsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<ICollection<CollectionVm>> Handle(GetCollectionsRequest request, CancellationToken cancellationToken)
    {
        var collections = _unitOfWork.CollectionRepository.GetCollections();
        
        return Task.FromResult
        (
            collections.Adapt<ICollection<CollectionVm>>()
        );
    }
}