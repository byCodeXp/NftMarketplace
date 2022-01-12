using FluentValidation;

namespace Web.Endpoints.Requests;

public record RegisterRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(request => request.UserName).NotEmpty();
        RuleFor(request => request.Password).NotEmpty();
    }
}