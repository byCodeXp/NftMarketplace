using FluentValidation;

namespace Web.Endpoints.Requests;

public record CreateCollectionRequest
{
    public string Name { get; set; }
}

public class CreateCollectionRequestValidator : AbstractValidator<CreateCollectionRequest>
{
    public CreateCollectionRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
    }
}