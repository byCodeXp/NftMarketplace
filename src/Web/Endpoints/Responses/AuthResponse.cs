using Web.Models;

namespace Web.Endpoints.Responses;

public record AuthResponse
{
    public string Token { get; set; }
    public UserVm User { get; set; }
}