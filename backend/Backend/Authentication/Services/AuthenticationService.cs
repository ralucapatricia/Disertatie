using API.Authentication.Exceptions;
using API.Authentication.Repositories;
using API.Utils.SQLQueries;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Authentication.Services
{
    public class AuthenticationService(IAuthenticationRepository _authenticationRepository, IConfiguration _configuration) : IAuthenticationService
    {
        private readonly string _jwtKey = _configuration["Jwt:Key"];
        private readonly string _jwtIssuer = _configuration["Jwt:Issuer"];
        private readonly string _jwtAudience = _configuration["Jwt:Audience"];

        public Models.SignInResponse Authenticate(string email, string password)
        {
            var user = _authenticationRepository.GetUserByEmailAndPassword(email, password);

            user = user ?? throw new UnauthorizedAccessException("Invalid credentials.");

            var token = GenerateJwtToken(user.UserId);
            var expirationDate = DateTime.UtcNow.AddHours(1);

            return new Models.SignInResponse
            {
                TokenId = Guid.NewGuid().ToString(), 
                UserId = user.UserId,
                Token = token,
                ExpirationDate = expirationDate
            };
        }

        public void SignUp(string email, string password)
        {
            if (_authenticationRepository.GetUserByEmailAndPassword(email, password) != null)
            {
                throw new UserAlreadyExistsException("An user with this Email already exists");
            }

            var user = new Models.User { Email = email, PasswordHash = password };
            _authenticationRepository.AddUser(user);
        }

        private string GenerateJwtToken(int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
