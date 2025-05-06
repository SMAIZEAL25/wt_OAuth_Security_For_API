using Newtonsoft.Json;

namespace Jwt_OAuth_Security_For_API.Authorization
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty("token_type")]
        public DateTime ExpireAt { get; set; }
    }
}
