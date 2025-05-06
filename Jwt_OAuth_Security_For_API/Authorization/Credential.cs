using System.ComponentModel.DataAnnotations;

namespace Jwt_OAuth_Security_For_API.Authorization
{
    public class Credential
    {
        
            [Required, Display(Name = "User name")]
            public string UserName { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Display(Name = "RememberMe")]
            public bool RememberMe { get; set; }
        
    }
}
