using Microsoft.AspNetCore.Mvc;
using Northwin.Models;
using Northwind.Api.Auth;
using Northwind.UnitOfWork;

namespace Northwind.Api.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUnitOfWork _unitOfWork;

        public TokenController(ITokenProvider tokenProvider, IUnitOfWork unitOfWork)
        {
            _tokenProvider = tokenProvider;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public JWT Post([FromBody]User userLogin)
        {
            var user = _unitOfWork.User.ValidateUser(userLogin.Email, userLogin.Password);
            if (user == null)
            {
                throw   new UnauthorizedAccessException();
            }

            var token = new JWT
            {
                Access_Token = _tokenProvider.CreateToken(user, DateTime.UtcNow.AddHours(8)),
                Expires_in = 480
            };

            return token;
        }





    }
}
