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
        private string _algoritm;
        private string _issuer;
        private string _audience;


        public JwtProvider(string issuer, string audience, string keyName)
        {
            var parameters = new CspParameters() { KeyContainerName = keyName};
            var provider = new RSACryptoServiceProvider(2048, parameters);
            _securityKey = new RsaSecurityKey(provider);
            _algoritm = SecurityAlgorithms.RsaSha512Signature;
            _issuer = issuer;
            _audience = audience;
        }

        public string CreateToken(User user, DateTime expiry)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Roles,
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString())

            }, "Custom"); 
        }

        public TokenValidationParameters GetValidationParameters()
        {
            throw new NotImplementedException();
        }
    }
}
