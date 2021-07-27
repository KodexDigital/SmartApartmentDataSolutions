using Application.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        public string GetAccessToken(string Email)
        {
            var payload = new Dictionary<string, object>
            {
               ["sub"] = Email,
               ["email"] = Email
            };
            return GetToken(payload);
        }

        public string GetToken(Dictionary<string, object> payload)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTOption.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(jWTOption.Issuer, jWTOption.Issuer,
              expires: DateTime.Now.AddMinutes(double.Parse(jWTOption.ExpirationTime)),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
