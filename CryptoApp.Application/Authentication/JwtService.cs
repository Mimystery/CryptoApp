using CryptoApp.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Application.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _options;

        public JwtService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            Console.WriteLine($"Jwt SecretKey: {_options.SecretKey}");
        }

        public string GenerateToken(TelegramUser tgUser)
        {
            //id,first_name,username,photo_url,

            var claims = new List<Claim>
            {
                new Claim("Id", tgUser.Id.ToString()),
                new Claim("FirstName", tgUser.FirstName ?? string.Empty),
                new Claim("UserName", tgUser.LastName ?? string.Empty),
                new Claim("PhotoUrl", tgUser.PhotoUrl ?? string.Empty),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours),
                claims: claims,
                signingCredentials: signingCredentials);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
