namespace API.Authentication.Repositories
{
    public interface IAuthenticationRepository
    {
        Models.User GetUserByEmailAndPassword(string email, string password);
        void AddUser(Models.User user);
    }
}
