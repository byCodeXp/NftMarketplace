using Application.Collections;
using Application.Exceptions;
using Domain;
using Domain.Entities.Identity;
using Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Controllers.Base;
using Web.Extensions;

namespace Web.Controllers;

public class AuthorController : ApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public AuthorController(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    [HttpGet("{authorId}/collections")]
    [Authorize(Roles = Env.Roles.User)]
    public async Task<ActionResult<ICollection<CollectionDto>>> GetCollections()
    {
        string userId = HttpContext.GetUserIdFromClaims();

        User user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            throw new BadRequestException("User does not exists");
        }

        ICollection<CollectionDto> collections = await _unitOfWork
            .CollectionRepository.GetCollections()
            .Where(collection => collection.Author.Id == user.Id)
            .ProjectToType<CollectionDto>()
            .ToListAsync();

        return Ok(collections);
    }
}