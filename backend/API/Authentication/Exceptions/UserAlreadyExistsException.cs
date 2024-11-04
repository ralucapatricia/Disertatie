namespace API.Authentication.Exceptions
{
    public class UserAlreadyExistsException(string message) : Exception(message)
    {
    }
}
