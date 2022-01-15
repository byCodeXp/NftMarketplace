using Application.Tokens;
using Application.Tokens.Commands;
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

public class TokensController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IPictureStorage _pictureStorage;

    public TokensController(IMediator mediator, IPictureStorage pictureStorage)
    {
        _mediator = mediator;
        _pictureStorage = pictureStorage;
    }

    [HttpGet("page/{page}/perPage/{perPage}")]
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
    
    [HttpPost("create")]
    [Authorize(Roles = Env.Roles.User)]
    public async Task<IActionResult> Create([FromBody] CreateTokenRequest request)
    {
        string fileName = await _pictureStorage.UploadImage(request.File, request.FileName);

        var command = request.Adapt<CreateTokenCommand>();

        command.Picture = fileName;

        var result = await _mediator.Send(command);

        return Created("Token", result);
    }
}