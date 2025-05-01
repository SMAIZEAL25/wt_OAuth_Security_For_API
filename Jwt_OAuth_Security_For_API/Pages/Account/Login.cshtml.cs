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
                        new Claim("Department", "HR")                      
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    // Sign in the user
                    await HttpContext.SignInAsync( "MyCookieAuth", claimsPrincipal);
                    return RedirectToPage("/Index");
                }
                return Page();
        }
    }

    public class Credential
    {
        [Required, Display(Name ="User name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
