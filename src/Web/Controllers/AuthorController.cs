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
    
    /// <summary>
    /// Get the author by id and get all his collections that he created
    /// </summary>
    /// <param name="userId">User unique identifier</param>
    [HttpGet("{userId}/collections")]
    [ProducesResponseType(typeof(ICollection<CollectionDto>), 200)]
    public async Task<IActionResult> GetCollections(Guid userId)
    {
        var query = new GetAuthorCollectionsQuery
        {
            Author = userId.ToString()
        };

        ICollection<CollectionDto> result = await _mediator.Send(query);
        
        return Ok(result);
    }
}