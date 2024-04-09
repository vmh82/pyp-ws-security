using Authorization.Domain.Services;
using Authorization.Entity.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Authorization.Infraestructure.Services
{
    public class AuthorizationInfraestructure : IAuthorizationInfraestructure
    {
        private readonly IConfiguration _config;

        public AuthorizationInfraestructure(IConfiguration config)
        {
            _config = config;
        }

        public JwtSecurityTokenResponseDto GenerateJwtToken(string roleType)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, roleType)
                }),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddSeconds(Convert.ToDouble(_config["Jwt:ExpirationSeconds"])),
                TokenType = "Bearer",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityToken jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(jwtSecurityToken);
            JwtSecurityTokenResponseDto jwtSecurity = new()
            {
                Token = token,
                IssuedAt = jwtSecurityToken.IssuedAt.ToLocalTime(),
                Expires = jwtSecurityToken.ValidTo.ToLocalTime(),
                TokenType = tokenDescriptor.TokenType
            };
            return jwtSecurity;
        }
    }
}
