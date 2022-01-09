using Domain.Entities.Base;
using Domain.Entities.Identity;

namespace Domain.Entities;

public record Token : Entity
{
    public string Name { get; set; }
    public string Picture { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// Navigation property: Collection which owned current token.
    /// </summary>
    public Collection Collection { get; set; }
    
    /// <summary>
    /// Navigation property: Author - user who create current token
    /// </summary>
    public User Author { get; set; }
}