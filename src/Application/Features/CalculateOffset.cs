namespace Application.Features;

public class CalculateOffset
{
    public string Type { get; set; }
    
    public static int Offset (int page, int perPage) => page <= 1 ? 0 : page * perPage - perPage;
}