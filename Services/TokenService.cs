using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementAPI.Context;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        internal static string GenerateJwtToken(User user)
        {
            /*
             * 1. Symmetric key creation
             * 2. Signing key with HmacSha256 algo
             * 3. creating a claim object
             * 4. create a token object using JwtSecurityToken
             * 5. serialize the token object into a string
             */
            var jwtSettings = _configuration.GetSection("Jwt");

            /*It takes the "Secret" value from your JWT configuration, converts it into a byte array using UTF-8 encoding.*/
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: credentials
            );

            /*A JwtSecurityTokenHandler is used to serialize the JwtSecurityToken object into its string representation.*/
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
