using MediatR;
using Web.Models;

namespace Web.Endpoints.Requests;

public record CreateTokenRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Collection { get; set; }
}