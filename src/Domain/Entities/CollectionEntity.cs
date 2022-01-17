﻿using Domain.Entities.Base;
using Domain.Entities.Identity;

namespace Domain.Entities;

public record CollectionEntity : Entity
{
    public string Name { get; set; }
    public string Cover { get; set; }

    /// <summary>
    /// Navigation property: Author - user who create this collection
    /// </summary>
    public User Author { get; set; }

    /// <summary>
    /// Navigation property: tokens which has current collection
    /// </summary>
    public ICollection<TokenEntity> Tokens { get; set; }
}