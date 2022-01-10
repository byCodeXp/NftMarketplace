using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using Web.Endpoints.Requests;
using Web.Models;

namespace Web.Controllers;

public class CollectionsController : ApiController
{
    private readonly IMediator _mediator;

    public CollectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<CollectionVm>> Create([FromBody] CreateCollectionRequest request)
    {
        CollectionVm result = await _mediator.Send(request);
        return Created("Collection", result);
    }
}