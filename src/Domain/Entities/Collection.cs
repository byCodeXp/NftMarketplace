using Domain.Entities.Base;
using Domain.Entities.Identity;

namespace Domain.Entities;

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