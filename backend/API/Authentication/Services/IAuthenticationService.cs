namespace API.Authentication.Services
{
    public interface IAuthenticationService
    {
        Models.SignInResponse Authenticate(string userEmail, string passwordHash);
        Models.SignInRequest SignUp(string userEmail, string passwordHash);
    }

}
