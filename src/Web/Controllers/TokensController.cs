using Application.Features.Tokens;
using Application.Features.Tokens.Commands;
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

namespace Web.Controllers;

public class TokensController : ApiController
{
    private readonly IMediator _mediator;

    public TokensController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Tokens pagination 
    /// </summary>
    /// <param name="page">Page number what you need</param>
    /// <param name="perPage">Count tokens on page what you need</param>
    [HttpGet("page/{page}/perPage/{perPage}")]
    [ProducesResponseType(typeof(TokensVm), 200)]
    public async Task<ActionResult<TokensVm>> Get(int page, int perPage)
    {
        var query = new GetTokensQuery
        {
            Page = page,
            PerPage = perPage
        };

        TokensVm result = await _mediator.Send(query);

        return Ok(result);
    }
    
    /// <summary>
    /// Create token and retrieve data
    /// </summary>
    [HttpPost("create")]
    [Authorize(Roles = Env.Roles.User)]
    [ProducesResponseType(typeof(TokenDto), 200)]
    public async Task<IActionResult> Create([FromBody] CreateTokenRequest request)
    {
        IStorage storage = new TokenPictureStorage();

        string fileName = await storage.SavePicture(request.File, request.FileName);

        var command = request.Adapt<CreateTokenCommand>() with
        {
            Picture = fileName
        };

        TokenDto token = await _mediator.Send(command);

        return Created("Token", token);
    }
}