using Application.Features.Collections;
using Application.Features.Collections.Commands;
using Application.Features.Collections.Queries;
using Application.Features.Tokens;
using Application.Features.Tokens.Queries;
using Domain;
using Infrastructure.Storage;
using Infrastructure.Storage.Base;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using Web.Endpoints.Requests;
using Web.Extensions;

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

    [HttpGet("{collectionId}/tokens/page/{page}/perPage/{perPage}")]
    public async Task<ActionResult<TokensVm>> Get(Guid collectionId, int page, int perPage)
    {
        var query = new GetCollectionTokensQuery
        {
            CollectionId = collectionId,
            Page = page,
            PerPage = perPage
        };

        TokensVm result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("create")]
    [Authorize(Roles = Env.Roles.User)]
    public async Task<ActionResult<CollectionDto>> Create([FromBody] CreateCollectionRequest request)
    {
        string userId = HttpContext.GetUserIdFromClaims();
        
        IStorage storage = new CollectionPictureStorage();

        string fileName = await storage.SavePicture(request.File, request.FileName);
        
        var command = request.Adapt<CreateCollectionCommand>() with
        {
            Cover = fileName,
            Author = Guid.Parse(userId)
        };

        CollectionDto collection = await _mediator.Send(command);
        
        return Created("Collection", collection);
    }
}