using Microsoft.IdentityModel.Tokens;
using Northwin.Models;

namespace Northwind.Api.Auth
{
    public interface ITokenProvider
    {
        string CreateToken(User user, DateTime expiry);
        TokenValidationParameters GetValidationParameters();
    }
}
