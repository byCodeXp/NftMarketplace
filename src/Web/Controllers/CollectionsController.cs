using Application.Collections;
using Application.Collections.Commands;
using Application.Collections.Queries;
using Domain;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("page/{page}/perPage/{perPage}")]
    public async Task<ActionResult<CollectionsVm>> Get(int page, int perPage)
    {
        var query = new GetCollectionsQuery
        {
            Page = page,
            PerPage = perPage
        };
        
        CollectionsVm result = await _mediator.Send(query);
        
        return Ok(result);
    }

    [HttpPost("create")]
    [Authorize(Roles = Env.Roles.User)]
    public async Task<ActionResult<CollectionDto>> Create([FromBody] CreateCollectionRequest request)
    {
        var command = request.Adapt<CreateCollectionCommand>();

        CollectionDto result = await _mediator.Send(command);
        
        return Created("Collection", result);
    }
}