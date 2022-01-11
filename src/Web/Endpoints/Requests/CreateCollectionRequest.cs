namespace Web.Endpoints.Requests;

public record CreateCollectionRequest
{
    public string Name { get; set; }
}