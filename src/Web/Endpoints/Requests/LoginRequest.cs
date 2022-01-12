using FluentValidation;

namespace Web.Endpoints.Requests;

public record LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool Remember { get; set; }
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.UserName).NotEmpty();
        RuleFor(request => request.Password).NotEmpty();
        RuleFor(request => request.Remember).NotNull();
    }
}