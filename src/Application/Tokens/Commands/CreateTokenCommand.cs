using Domain.Entities;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Tokens.Commands;

public class CreateTokenCommand : IRequest<TokenDto>, BaseRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Picture { get; set; }
}

public class CreateTokenHandler : IRequestHandler<CreateTokenCommand, TokenDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTokenHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TokenDto> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var token = request.Adapt<Token>();

        await _unitOfWork.TokenRepository.AddToken(token);
        await _unitOfWork.Completed(cancellationToken);

        return token.Adapt<TokenDto>();
    }
}