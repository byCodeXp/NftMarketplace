using FluentValidation;

namespace Web.Endpoints.Requests;

public record CreateTokenRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Collection { get; set; }
    
    public string File { get; set; }
    public string FileName { get; set;}
}

public class CreateTokenRequestValidator : AbstractValidator<CreateTokenRequest>
{
    public CreateTokenRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Collection).NotEmpty();
        
        RuleFor(request => request.File).NotNull();
        RuleFor(request => request.FileName).NotEmpty();
    }
}