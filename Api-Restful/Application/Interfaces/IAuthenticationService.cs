namespace Api_Restful.Application.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateToken(string userId, string role);
        bool ValidateToken(string token);
    }
}
