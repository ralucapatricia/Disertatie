using API.Authentication.Exceptions;
using API.Authentication.Repositories;
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
            var signInRequest = _authenticationRepository.GetUserByEmailAndPassword(email, password);

            signInRequest = signInRequest ?? throw new UnauthorizedAccessException("Invalid credentials.");

            var token = GenerateJwtToken(signInRequest.UserId);
            var expirationDate = DateTime.UtcNow.AddHours(1);

            return new Models.SignInResponse
            {
                TokenId = Guid.NewGuid().ToString(), 
                UserId = signInRequest.UserId,
                Token = token,
                ExpirationDate = expirationDate
            };
        }

        public Models.SignInRequest SignUp(string userEmail, string password)
        {
            if (_authenticationRepository.GetUserByEmailAndPassword(userEmail, password) != null)
            {
                throw new UserAlreadyExistsException("An user with this Email already exists");
            }

            var signInRequest = new Models.SignInRequest { UserEmail = userEmail, PasswordHash = password };
            _authenticationRepository.AddUser(signInRequest);
            return signInRequest;
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
