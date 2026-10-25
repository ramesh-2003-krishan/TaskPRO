using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskPRO.Application.interfaces;
using TaskPRO.Domain.entities;
using System.Security.Cryptography;

namespace TaskPRO.Infrastructure.authentication
{
    public class JwtTokenGenarator : IJwtTokenGenarator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenarator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user, string roleName)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var secretKey = jwtSettings["SecretKey"];

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT secret key is not configured.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.UserEmail ?? string.Empty),
                new Claim(ClaimTypes.Role, roleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["AccessTokenExpirationInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(Guid userId)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(jwtSettings["RefreshTokenExpirationInDays"]));
            expires = expires.ToUniversalTime();
            

            var randomNumber = new byte[32];
            using var ran = RandomNumberGenerator.Create();
            ran.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = expires,
                Created = DateTime.UtcNow,
                UserId = userId
            };
        }
    }
}