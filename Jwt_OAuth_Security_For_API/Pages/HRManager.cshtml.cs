using Jwt_OAuth_Security_For_API.Authorization;
using Jwt_OAuth_Security_For_API.DTO;
using Jwt_OAuth_Security_For_API.Pages.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Jwt_OAuth_Security_For_API.Pages
{
    [Authorize (Policy ="HRManagerOnly")]
    public class HRManagerModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;


        [BindProperty]
        public List<WeatherForeCastDTO> WeatherForeCastItems { get; set; } = new List<WeatherForeCastDTO>();

        public HRManagerModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task OnGetasync() 
         {
            //get token from the session 
            JwtToken tokens = new JwtToken();

            var strtokenobjt = HttpContext.Session.GetString("access_token");
            if (string.IsNullOrEmpty(strtokenobjt))
            {
                //    tokens = JsonConvert.DeserializeObject<JwtToken>(strtokenobjt);
                tokens = await Authenticate();

            } else
            {
                tokens = JsonConvert.DeserializeObject<JwtToken>(strtokenobjt) ?? new JwtToken();
            }

            if (tokens == null || 
                string.IsNullOrWhiteSpace(tokens.AccessToken) 
                || tokens.ExpireAt <= DateTime.UtcNow)
            {
                tokens = await Authenticate();
            }
            var httpclient = httpClientFactory.CreateClient("OurWebAPI");
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens?.AccessToken ?? string.Empty);
            WeatherForeCastItems = await httpclient.GetFromJsonAsync<List<WeatherForeCastDTO>>("WeatherForecast") ?? new List<WeatherForeCastDTO>();
           
        }
         
        private async Task<JwtToken> Authenticate()
        {
            var httpclient = httpClientFactory.CreateClient("OurWebAPI");
            var response = await httpclient.PostAsJsonAsync("auth", new Credential { UserName = "admin", Password = "admin" });
            response.EnsureSuccessStatusCode();
            string strjwt = await response.Content.ReadAsStringAsync();
            HttpContext.Session.SetString("access_token", strjwt);
            return JsonConvert.DeserializeObject<JwtToken>(strjwt) ?? new JwtToken();
        }
    }
}
