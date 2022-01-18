using Application.Features.Collections;
using Application.Features.Collections.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using Web.Extensions;

namespace Web.Controllers;

public class AuthorController : ApiController
{
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{authorId}/collections")]
    [Authorize(Roles = Env.Roles.User)]
    public async Task<ActionResult<ICollection<CollectionDto>>> GetCollections()
    {
        string userId = HttpContext.GetUserIdFromClaims();
        
        var query = new GetAuthorCollectionsQuery { Author = userId };

        ICollection<CollectionDto> result = await _mediator.Send(query);
        
        return Ok(result);
    }
}