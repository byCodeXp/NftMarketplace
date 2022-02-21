using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class User : IdentityUser<Guid>
{
    public User(string userName)
        : base(userName)
    {
    }

    /// <summary>
    /// Navigation property: tokens which created current user
    /// </summary>
    public ICollection<TokenEntity> Tokens { get; set; }

    /// <summary>
    /// Navigation property: collections which created current user
    /// </summary>
    public ICollection<CollectionEntity> Collections { get; set; }
}