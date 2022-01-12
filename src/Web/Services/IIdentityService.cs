using Web.Endpoints.Requests;
using Web.Endpoints.Responses;
using Web.Models;

namespace Web.Services;

public interface IIdentityService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);

    Task<AuthResponse> LoginAsync(LoginRequest request);

    Task<UserVm> GetUserAsync(string userId);
}