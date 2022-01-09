namespace Domain.Entities.Base;

public abstract record Entity
{
    public Guid Id { get; set; }
}