using Domain.Entities;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Collections.Commands;

public class CreateCollectionCommand : IRequest<CollectionDto>, BaseRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Collection { get; set; }
}

public class CreateCollectionHandler : IRequestHandler<CreateCollectionCommand, CollectionDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCollectionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CollectionDto> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = new CollectionEntity
        {
            Name = request.Name
        };

        await _unitOfWork.CollectionRepository.AddCollection(collection);
        await _unitOfWork.Completed(cancellationToken);

        return collection.Adapt<CollectionDto>();
    }
}