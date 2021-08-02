using Application.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Implementations
{
    public class Tokenization : ITokenization
    {
        private readonly JWTSettings jWTOption;
        public Tokenization(IOptions<JWTSettings> jwtOptions)
        {
            jWTOption = jwtOptions.Value;
        }
        public string GetAccessToken(string userEmail) => GetToken(userEmail);

        public string GetToken(string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTOption.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(jWTOption.Issuer, jWTOption.Issuer,claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
