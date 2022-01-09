namespace Web.Endpoints.Requests;

public record LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool Remember { get; set; }
}