using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Tokens.Commands;

public class CreateTokenCommand : IRequest<TokenDto>, BaseRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Picture { get; set; }

    public Guid Author { get; set; }
    public Guid Collection { get; set; }
}

public class CreateTokenHandler : IRequestHandler<CreateTokenCommand, TokenDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public CreateTokenHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<TokenDto> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        string authorId = request.Author.ToString();

        User author = await _userManager.FindByIdAsync(authorId);

        if (author is null)
        {
            throw new BadRequestException("User doesn't exists");
        }

        CollectionEntity collection = await _unitOfWork.CollectionRepository.FindCollection(request.Collection, cancellationToken);
        
        if (collection is null)
        {
            throw new BadRequestException("Collection doesn't exists");
        }
        
        var token = request.Adapt<TokenEntity>() with
        {
            Author = author,
            Collection = collection
        };

        await _unitOfWork.TokenRepository.AddToken(token);
        await _unitOfWork.Completed(cancellationToken);

        return token.Adapt<TokenDto>();
    }
}