using Application.Dtos;
using MediatR;

namespace Application.Services.User.Commands.CurrentUser
{
    public class CurrentUserCommand: IRequest<UserDto>
    {
    }
}