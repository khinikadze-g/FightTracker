using FightTracker.Application.UserServices.cs;
using FightTracker.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Infrastructure.Auth
{
    public class TokenGenerator(IConfiguration configuration) : ITokenGenerator
    {
        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken
            (
                issuer: configuration["JwtSettings:Issuer"],
                audience : configuration["JwtSettings:Audience"],
                claims: claims,
                expires : DateTime.UtcNow.AddMinutes(30),
                signingCredentials : creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
