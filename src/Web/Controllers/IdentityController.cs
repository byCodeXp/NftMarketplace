using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using Web.Endpoints.Requests;
using Web.Endpoints.Responses;
using Web.Extensions;
using Web.Models;
using Web.Services;

namespace Web.Controllers;

public class IdentityController : ApiController
{
    private readonly IdentityService _identityService;

    public IdentityController(IdentityService identityService)
    {
        _identityService = identityService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        return await _identityService.RegisterAsync(request);
    }
        
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        return await _identityService.LoginAsync(request);
    }

    [HttpGet("user")]
    [Authorize(Roles = Env.Roles.User)]
    public async Task<ActionResult<UserVm>> GetUser()
    {
        var userId = HttpContext.GetUserIdFromClaims();
        return await _identityService.GetUserAsync(userId);
    }
}