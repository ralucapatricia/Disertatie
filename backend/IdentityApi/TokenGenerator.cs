using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityApi
{
    public class TokenGenerator
    {
        public string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("ThisIsASecretKeyThatIs32BytesLong!");


            var claims = new List<Claim>
            { 
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new(JwtRegisteredClaimNames.Sub, email),
                new(JwtRegisteredClaimNames.Email, email)
           };


            var tokenDescriptor = new SecurityTokenDescriptor
            { 
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                 SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),            
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

    }
}
