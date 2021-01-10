using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using AppContext = Application.Infrastructure.AppContext;

namespace Application.Services.User.Commands.CurrentUser
{
    public class CurrentUserCommandHandler : IRequestHandler<CurrentUserCommand, UserDto>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IAppDbContext _context;
        private readonly ICookieService _cookieService;

        public CurrentUserCommandHandler(IJwtGenerator jwtGenerator, IAppDbContext context,
            ICookieService cookieService)
        {
            _jwtGenerator = jwtGenerator;
            _context = context;
            _cookieService = cookieService;
        }

        public async Task<UserDto> Handle(CurrentUserCommand request, CancellationToken cancellationToken)
        {
            var token = AppContext.Current.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var refreshToken = AppContext.Current.Request.Cookies[Constants.RefreshToken];
            
            var principal = _jwtGenerator.GetPrincipalFromExpiredToken(token);

            var username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users
                .Where(x => x.UserName == username)
                .Include(x => x.RefreshTokens)
                .SingleOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == refreshToken),
                    cancellationToken);

            if (user == null)
            {
                ThrowUnauthorized();
            }

            var oldToken = user!.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

            if (oldToken != null && !oldToken.IsActive)
            {
                ThrowUnauthorized();
            }

            if (oldToken != null)
            {
                oldToken.Revoked = DateTime.UtcNow;
            }

            var newRefreshToken = _jwtGenerator.GenerateRefreshToken(user);

            await _context.RefreshTokens.AddAsync(newRefreshToken, cancellationToken);

            var revokedTokens = user.RefreshTokens.Where(x => x.IsExpired);

            _context.RefreshTokens.RemoveRange(revokedTokens);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success)
            {
                _cookieService.SetTokenCookie(newRefreshToken.Token);

                return new UserDto
                {
                    UserName = user.UserName,
                    Token = _jwtGenerator.GenerateAccessToken(user),
                    RefreshToken = newRefreshToken.Token
                };
            }

            throw new Exception(Constants.ServerSavingError);
        }

        private static void ThrowUnauthorized()
        {
            AppContext.Current.Response.Headers.Add(Constants.TokenExpired, "true");
            throw new RestException(HttpStatusCode.Unauthorized);
        }
    }
    
}