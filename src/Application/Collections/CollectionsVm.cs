namespace Application.Collections;

public record CollectionsVm
{
    public int TotalCount { get; set; }
    public ICollection<CollectionDto> Collections { get; set; }
}