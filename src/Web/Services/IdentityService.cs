using Application.Exceptions;
using Domain;
using Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Web.Endpoints.Requests;
using Web.Endpoints.Responses;
using Web.Helpers;
using Web.Models;

namespace Web.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtHelper _jwtHelper;

    public IdentityService(UserManager<User> userManager, JwtHelper jwtHelper)
    {
        _userManager = userManager;
        _jwtHelper = jwtHelper;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        User user = request.Adapt<User>();

        IdentityResult createNewUserResult  = await _userManager.CreateAsync(user, request.Password);

        if (!createNewUserResult .Succeeded)
        {
            throw new BadRequestException("Invalid credentials");
        }

        await _userManager.AddToRoleAsync(user, Env.Roles.User);
        
        return await GenerateAuthResponseAsync(user, Env.Identity.TokenExpirationTime.OneDay);
    }
    
    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        User user = await _userManager.FindByNameAsync(request.UserName);
        
        if (user is null)
        {
            throw new BadRequestException("Invalid credentials");
        }
        
        bool passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        
        if (!passwordValid)
        {
            throw new BadRequestException("Invalid credentials");
        }
        
        TimeSpan tokenDuration = Env.Identity.TokenExpirationTime.OneDay;

        if (request.Remember)
        {
            tokenDuration = Env.Identity.TokenExpirationTime.SevenDays;
        }

        return await GenerateAuthResponseAsync(user, tokenDuration);
    }
    
    public async Task<UserVm> GetUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            throw new BadRequestException("User was not found");
        }

        return user.Adapt<UserVm>();
    }

    private async Task<AuthResponse> GenerateAuthResponseAsync(User user, TimeSpan tokenLifetime)
    {
        string roles = string.Join(",", await _userManager.GetRolesAsync(user));
        
        string token = _jwtHelper.GenerateToken(user.Id.ToString(), roles, tokenLifetime);
        
        return new AuthResponse
        {
            Token = token,
            User = user.Adapt<UserVm>()
        };
    }
}