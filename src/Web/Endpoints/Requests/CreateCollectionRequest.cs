using MediatR;
using Web.Models;

namespace Web.Endpoints.Requests;

public record CreateCollectionRequest : IRequest<CollectionVm>
{
    public string Name { get; set; }
}