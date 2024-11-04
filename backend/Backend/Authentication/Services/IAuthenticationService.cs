using API.Models;

namespace API.Authentication.Services
{
    public interface IAuthenticationService
    {
        SignInResponse Authenticate(string email, string password);
        void SignUp(string email, string password);
    }

}
