using Jwt_OAuth_Security_For_API.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace Jwt_OAuth_Security_For_API.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; } = new Credential();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync ()
        {
            if (!ModelState.IsValid) return Page();

            if (Credential.UserName == "admin" && Credential.Password == "admin")
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

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                // Sign in the user

                var authproperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe
                };

                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authproperties );
                    return RedirectToPage("/Index");
                }
                return Page();
        }
    }

  
}
