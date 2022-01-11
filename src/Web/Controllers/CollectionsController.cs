using Application.Collections;
using Application.Collections.Commands;
using Application.Collections.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using Web.Endpoints.Requests;

namespace Web.Controllers;

public class CollectionsController : ApiController
{
    private readonly IMediator _mediator;

    public CollectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetCollectionsQuery();
        
        ICollection<CollectionDto> result = await _mediator.Send(query);
        
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCollectionRequest request)
    {
        var command = request.Adapt<CreateCollectionCommand>();

        CollectionDto result = await _mediator.Send(command);
        
        return Created("Collection", result);
    }
}