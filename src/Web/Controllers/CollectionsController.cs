using Application.Collections;
using Application.Collections.Commands;
using Application.Collections.Queries;
using Application.Tokens;
using Application.Tokens.Queries;
using Domain;
using Infrastructure.Storage;
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
    private readonly IPictureStorage _pictureStorage;

    public CollectionsController(IMediator mediator, IPictureStorage pictureStorage)
    {
        _mediator = mediator;
        _pictureStorage = pictureStorage;
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
        string fileName = await _pictureStorage.UploadImage(request.File, request.FileName);
        
        var command = request.Adapt<CreateCollectionCommand>() with
        {
            Cover = fileName
        };

        CollectionDto result = await _mediator.Send(command);
        
        return Created("Collection", result);
    }
}