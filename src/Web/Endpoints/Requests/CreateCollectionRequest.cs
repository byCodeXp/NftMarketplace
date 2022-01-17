using FluentValidation;

namespace Web.Endpoints.Requests;

public record CreateCollectionRequest
{
    public string Name { get; set; }
    
    public string File { get; set; }
    public string FileName { get; set; }
}

public class CreateCollectionRequestValidator : AbstractValidator<CreateCollectionRequest>
{
    public CreateCollectionRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.File).NotEmpty();
        RuleFor(request => request.FileName).NotEmpty();
    }
}