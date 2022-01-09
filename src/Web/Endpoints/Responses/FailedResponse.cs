using System.Text.Json;

namespace Web.Endpoints.Responses;

public record FailedResponse
{
    public int Code { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
};