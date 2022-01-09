using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;
 
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Navigation property: tokens which created current user
    /// </summary>
    public ICollection<Token> Tokens { get; set; }
    
    /// <summary>
    /// Navigation property: collections which created current user
    /// </summary>
    public ICollection<Collection> Collections { get; set; }
}