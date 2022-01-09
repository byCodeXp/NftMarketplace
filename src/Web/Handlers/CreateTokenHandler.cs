using Domain.Entities;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Web.Endpoints.Requests;
using Web.Exceptions;
using Web.Models;

namespace Web.Handlers;

public class CreateTokenHandler : IRequestHandler<CreateTokenRequest, TokenVm>
{
    private readonly DataContext _context;

    public CreateTokenHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<TokenVm> Handle(CreateTokenRequest request, CancellationToken cancellationToken)
    {
        Collection collection = await _context.FindAsync<Collection>(request.Collection);

        if (collection is null)
        {
            throw new BadRequestRestException("Collection is not defined");
        }
        
        Token token = request.Adapt<Token>() with { Collection = collection };

        await _context.AddAsync(token, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return token.Adapt<TokenVm>();
    }
}