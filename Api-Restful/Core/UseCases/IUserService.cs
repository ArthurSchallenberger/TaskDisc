namespace Api_Restful.Core.UseCases
{
    public interface IUserService
    {
        string Login(string username, string password);
    }
}
