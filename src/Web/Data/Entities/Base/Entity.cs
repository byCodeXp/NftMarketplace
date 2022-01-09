namespace Web.Data.Entities.Base;

public abstract record Entity
{
    public Guid Id { get; set; }
}