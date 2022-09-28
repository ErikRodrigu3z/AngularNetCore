using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Northwin.Models;
using Northwind.Api.Auth;
using Northwind.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Northwind.Api.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        public IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public TokenController(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Post([FromBody]User userLogin)
        {
            try
            {
                var user = _unitOfWork.User.ValidateUser(userLogin.Email, userLogin.Password);
                if (user == null)
                {
                    return NotFound("User or password wrong");
                }

                //create claims details based on the user information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Role", user.Roles),
                        //new Claim("UserName", userLogin.UserName),
                        new Claim("Email", user.Email)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signIn);

                var resToken = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(JsonConvert.SerializeObject(resToken));
            }
            catch (Exception ex)
            {

                throw;
            }
        }





    }
}
