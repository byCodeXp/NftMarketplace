using Application.Tokens;
using Application.Tokens.Commands;
using Application.Tokens.Queries;
using Domain;
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

    [HttpGet]
    public async Task<ActionResult<ICollection<TokenDto>>> Get()
    {
        var query = new GetTokensQuery();

        ICollection<TokenDto> result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("create")]
    [Authorize(Roles = Env.Roles.User)]
    public async Task<ActionResult<TokenDto>> Create([FromBody] CreateTokenRequest request)
    {
        var command = request.Adapt<CreateTokenCommand>();
        
        TokenDto result = await _mediator.Send(command);
        
        return Created("Token", result);
    }
}