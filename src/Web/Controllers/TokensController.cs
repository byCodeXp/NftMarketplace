using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using Web.Endpoints.Requests;

namespace Web.Controllers;

public class TokensController : ApiController
{
    private readonly IMediator _mediator;

    public TokensController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Token>> Create([FromBody] CreateTokenRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}