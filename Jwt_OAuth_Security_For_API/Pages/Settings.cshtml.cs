using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jwt_OAuth_Security_For_API.Pages
{
    [Authorize (Policy = "AdminOnlys") ]
    public class SettingsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
