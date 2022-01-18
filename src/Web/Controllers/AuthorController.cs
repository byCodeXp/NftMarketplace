using Application.Features.Collections;
using Application.Features.Collections.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers;

public class AuthorController : ApiController
{
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{authorId}/collections")]
    public async Task<ActionResult<ICollection<CollectionDto>>> GetCollections(string authorId)
    {
        var query = new GetAuthorCollectionsQuery
        {
            Author = authorId
        };

        ICollection<CollectionDto> result = await _mediator.Send(query);
        
        return Ok(result);
    }
}