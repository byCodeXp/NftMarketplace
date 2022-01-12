using FluentValidation;

namespace Web.Endpoints.Requests;

public record CreateTokenRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Collection { get; set; }
}

public class CreateTokenRequestValidator : AbstractValidator<CreateTokenRequest>
{
    public CreateTokenRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
    }
}