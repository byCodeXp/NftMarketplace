using Application.Exceptions;
using Domain.Entities;
using Domain.Entities.Identity;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Collections.Commands;

public class CreateCollectionCommand : IRequest<CollectionDto>, BaseRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Author { get; set; }
}

public class CreateCollectionHandler : IRequestHandler<CreateCollectionCommand, CollectionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public CreateCollectionHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<CollectionDto> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
    {
        string authorId = request.Author.ToString();

        User author = await _userManager.FindByIdAsync(authorId);

        if (author is null)
        {
            throw new BadRequestException("User doesn't exists");
        }
        
        var collection = new CollectionEntity
        {
            Name = request.Name,
            Author = author
        };

        await _unitOfWork.CollectionRepository.AddCollection(collection);
        await _unitOfWork.Completed(cancellationToken);

        return collection.Adapt<CollectionDto>();
    }
}