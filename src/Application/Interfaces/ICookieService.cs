namespace Application.Interfaces
{
    public interface ICookieService
    {
        void SetTokenCookie(string refreshToken);
    }
}