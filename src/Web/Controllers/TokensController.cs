using Application.Tokens;
using Application.Tokens.Commands;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = Env.Roles.User)]
    public async Task<ActionResult<Token>> Create([FromBody] CreateTokenRequest request)
    {
        var command = request.Adapt<CreateTokenCommand>();
        
        TokenDto result = await _mediator.Send(command);
        
        return Created("Token", result);
    }
}