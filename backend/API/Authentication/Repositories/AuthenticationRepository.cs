namespace API.Authentication.Repositories
{
    public class AuthenticationRepository(Data.StoreContext _storeContext) : IAuthenticationRepository
    {
        public Models.SignInRequest GetUserByEmailAndPassword(string userEmail, string passwordHash)
        {
            return _storeContext.Users.SingleOrDefault(u => u.UserEmail == userEmail && u.PasswordHash == passwordHash);
        }
        
        public Models.SignInRequest AddUser(Models.SignInRequest signInRequest)
        {
            _storeContext.Users.Add(signInRequest);
            _storeContext.SaveChanges();
            return signInRequest;
        }
    }
}
