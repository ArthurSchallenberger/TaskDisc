namespace TaskDisc.Application.Interfaces
{
    public interface IAuthService
    {

        Task<string> AuthenticateToken(string email, string password);
        //Task<bool> Register(string email, string password, string jobTitle);
        Task<bool> ValidateToken(string token);
        //Task<string> RefreshToken(string token);
        //Task<bool> RevokeToken(string token);
    }
}
