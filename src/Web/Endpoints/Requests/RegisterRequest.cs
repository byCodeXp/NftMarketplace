namespace Web.Endpoints.Requests;

public record RegisterRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}