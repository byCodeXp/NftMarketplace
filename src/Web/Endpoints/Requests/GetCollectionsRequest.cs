using MediatR;
using Web.Models;

namespace Web.Endpoints.Requests;

public record GetCollectionsRequest : IRequest<ICollection<CollectionVm>>
{
    
}