using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

public record GetUserCommand : IRequest<GetUserResult>
{
    public Guid Id { get; }

    public GetUserCommand(Guid id)
    {
        Id = id;
    }
}
