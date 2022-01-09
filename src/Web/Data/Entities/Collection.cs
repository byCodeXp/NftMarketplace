using Web.Data.Entities.Base;
using Web.Data.Identity;

namespace Web.Data.Entities;

public record Collection : Entity
{
    public string Name { get; set; }

    /// <summary>
    /// Navigation property: Author - user who create this collection
    /// </summary>
    public User Author { get; set; }

    /// <summary>
    /// Navigation property: tokens which has current collection
    /// </summary>
    public ICollection<Token> Tokens { get; set; }
}