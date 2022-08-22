using Microsoft.IdentityModel.Tokens;
using Northwin.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Northwind.Api.Auth
{
    public class JwtProvider : ITokenProvider
    {
        private RsaSecurityKey _securityKey;
        private string _algorithm;
        private string _issuer;
        private string _audience;


        public JwtProvider(string issuer, string audience, string keyName)
        {
            var parameters = new CspParameters() { KeyContainerName = keyName};
            var provider = new RSACryptoServiceProvider(2048, parameters);
            _securityKey = new RsaSecurityKey(provider);
            _algorithm = SecurityAlgorithms.RsaSha512Signature;
            _issuer = issuer;
            _audience = audience;
        }

        public string CreateToken(User user, DateTime expiry)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Roles),
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString())

            }, "Custom");

            SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = _audience,
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(_securityKey, _algorithm),
                Expires = expiry.ToUniversalTime(),
                Subject = identity
            });

            return tokenHandler.WriteToken(token);
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                IssuerSigningKey = _securityKey,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ClockSkew   = TimeSpan.FromSeconds(0)
            };
        }
    }
}
