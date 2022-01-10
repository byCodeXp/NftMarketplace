using Domain.Entities;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Web.Endpoints.Requests;
using Web.Models;

namespace Web.Handlers;

public class CreateCollectionHandler : IRequestHandler<CreateCollectionRequest, CollectionVm>
{
    private readonly DataContext _context;

    public CreateCollectionHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<CollectionVm> Handle(CreateCollectionRequest request, CancellationToken cancellationToken)
    {
        Collection collection = request.Adapt<Collection>();

        await _context.Collections.AddAsync(collection, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return collection.Adapt<CollectionVm>();
    }
}