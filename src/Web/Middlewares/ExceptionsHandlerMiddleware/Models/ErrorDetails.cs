using System.Text.Json;

namespace Web.Middlewares.ExceptionsHandlerMiddleware.Models;

public record ErrorDetails
{
    public string Message { get; set; }
    public int Code { get; set; }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
};