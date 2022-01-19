using Domain;
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
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    /// <summary>
    /// Create new user and retrieve data and token
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), 200)]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        return await _identityService.RegisterAsync(request);
    }
    
    /// <summary>
    /// Find user and check password then retrieve token and user data
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), 200)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        return await _identityService.LoginAsync(request);
    }

    /// <summary>
    /// Retrieve authorized user data
    /// </summary>
    [HttpGet("user")]
    [Authorize(Roles = Env.Roles.User)]
    [ProducesResponseType(typeof(UserVm), 200)]
    public async Task<ActionResult<UserVm>> GetUser()
    {
        var userId = HttpContext.GetUserIdFromClaims();
        return await _identityService.GetUserAsync(userId);
    }
}