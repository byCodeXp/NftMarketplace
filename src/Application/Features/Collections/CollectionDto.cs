namespace Application.Features.Collections;

public record CollectionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Cover { get; set; }
}