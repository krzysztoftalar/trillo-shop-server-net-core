using System;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Infrastructure
{
    public class CookieService : ICookieService
    {
        private readonly IResponseCookies _responseCookies;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _responseCookies = httpContextAccessor.HttpContext.Response.Cookies;
        }

        public void SetTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Secure = true
            };

            _responseCookies.Append(Constants.RefreshToken, refreshToken, cookieOptions);
        }
    }
}