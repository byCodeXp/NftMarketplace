namespace Domain.Entities.Base;

public abstract record Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedTimeStamp { get; set; }
    public DateTime UpdatedTimeStamp { get; set; }
}