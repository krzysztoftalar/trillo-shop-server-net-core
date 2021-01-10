using System.Threading.Tasks;
using Application.Dtos;
using Application.Services.User.Commands.CurrentUser;
using Application.Services.User.Commands.Logout;
using Application.Services.User.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class UsersController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginUserQuery query)
        {
            return await Mediator.Send(query);
        }

        [AllowAnonymous]
        [HttpPost("current")]
        public async Task<ActionResult<UserDto>> CurrentUser(CurrentUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<Unit>> Logout(LogoutUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}