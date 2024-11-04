namespace API.Authentication.Repositories
{
    public class AuthenticationRepository(Data.StoreContext _context) : IAuthenticationRepository
    {
        public Models.User GetUserByEmailAndPassword(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email && u.PasswordHash == password);
        }
        
        public void AddUser(Models.User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
