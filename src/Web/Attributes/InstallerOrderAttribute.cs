namespace Web.Attributes;

public class InstallerOrderAttribute : Attribute
{
    public int Order { get; }
    
    public InstallerOrderAttribute(int order)
    {
        Order = order;
    }
}