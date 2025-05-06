using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] Credential credential)
        {
            if (credential.UserName == "admin" && credential.Password == "admin")
            {


                var claims = new List<Claim>
                     {
                        new Claim(ClaimTypes.Name, "admin"),
                        new Claim(ClaimTypes.Email,"admin@mysite.com"),
                        new Claim("Department", "HR"),
                        new Claim("Admin", "true"),
                        new Claim("Manager", "true"),
                        new Claim("EmployementDate", "2025-01-03")
                    };

                var expiresAt = DateTime.UtcNow.AddMinutes(10);

                return Ok(new
                {
                    access_token = createToken(claims, expiresAt),
                    expiresAt = expiresAt,
                });


            }

            ModelState.AddModelError("UnAuthorized", "You are not authorized to access the endpoint.");
            return Unauthorized(ModelState);
        }

        private string createToken(IEnumerable<Claim> claims, DateTime expireAt)
        {
           var seceretkey = Encoding.ASCII.GetBytes (_configuration.GetValue<string>("SeceretKey"));

            var jwt = new JwtSecurityToken(
                //issuer: "https://localhost:5001",
                //audience: "https://localhost:5001",
                //notAfter: DateTime.UtcNow,
                claims: claims,
                expires: expireAt,
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(seceretkey),
                    SecurityAlgorithms.HmacSha256Signature));
                
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }

    public class Credential
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
