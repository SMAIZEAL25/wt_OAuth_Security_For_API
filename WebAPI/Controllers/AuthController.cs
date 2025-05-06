using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
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
                    access_token = "",
                    expiresAt = expiresAt,
                });


            }

            ModelState.AddModelError("UnAuthorized", "You are not authorized to access the endpoint.");
            return Unauthorized(ModelState);
        }

        private string createToken(IEnumerable<Claim> claims, DateTime expireAt)
        {
            // Generate the JWT 
        }
    }

    public class Credential
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
