namespace TaskDisc.Application.Interfaces
{
    public interface IJwtAuthenticationService
    {
        string GenerateToken(string userId, string role);
        bool ValidateToken(string token);
    }
}
