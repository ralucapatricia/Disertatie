namespace API.Authentication.Repositories
{
    public interface IAuthenticationRepository
    {
        Models.SignInRequest GetUserByEmailAndPassword(string userEmail, string passwordHash);
        Models.SignInRequest AddUser(Models.SignInRequest signInRequest);
    }
}
