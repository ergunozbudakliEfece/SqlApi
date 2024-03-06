using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SqlApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SqlApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
       
        readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            
            _configuration = configuration;
        }
     
        [HttpPost("Login")]
        public Task<IActionResult> Login([FromForm] UserLogin userLogin)
        {
            if (userLogin.Email == "ergunozbudakli@efecegalvaniz.com" && userLogin.Password == "begum142088")
            {

                string token = GenerateJwtToken(userLogin);
                return System.Threading.Tasks.Task.FromResult<IActionResult>(Ok(token));
            }
            //Token üretiliyor.

            return System.Threading.Tasks.Task.FromResult<IActionResult>(Unauthorized());
        }
        [HttpPost("LoginSuperNova")]
        public Task<IActionResult> LoginNova([FromForm] UserLogin userLogin)
        {
            if (userLogin.Email == "supernova" && userLogin.Password == "sprmb2023")
            {

                string token = GenerateJwtToken(userLogin);
                return System.Threading.Tasks.Task.FromResult<IActionResult>(Ok(token));
            }
            //Token üretiliyor.

            return System.Threading.Tasks.Task.FromResult<IActionResult>(Unauthorized());
        }

        private string GenerateJwtToken(UserLogin userLogin)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,userLogin.Email),
                    new Claim(ClaimTypes.Name,userLogin.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescription);
            return tokenhandler.WriteToken(token);
        }
        
    }
}
