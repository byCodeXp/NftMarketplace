using Application.Exceptions;
using Domain.Entities.Identity;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Collections.Queries;

public record GetAuthorCollectionsQuery : IRequest<ICollection<CollectionDto>>, BaseRequest
{
    public string Author { get; set; }
}

public class GetAuthorCollectionsHandler : IRequestHandler<GetAuthorCollectionsQuery, ICollection<CollectionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public GetAuthorCollectionsHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<ICollection<CollectionDto>> Handle(GetAuthorCollectionsQuery request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByIdAsync(request.Author);

        if (user is null)
        {
            throw new BadRequestException("User does not exists");
        }

        _unitOfWork.Include(user, entity => entity.Collections);
        
        var collections = user.Collections.Adapt<ICollection<CollectionDto>>();
        
        return collections;
    }
}