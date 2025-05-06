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
            var httpclient = httpClientFactory.CreateClient("OurWebAPI");
            var response = await httpclient.PostAsJsonAsync("auth", new Credential { UserName = "admin", Password = "admin" });
             response.EnsureSuccessStatusCode();
            string strjwt = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtToken>(strjwt);

            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken?? string.Empty);
            WeatherForeCastItems = await  httpclient.GetFromJsonAsync<List<WeatherForeCastDTO>>("WeatherForecast") ?? new List<WeatherForeCastDTO>();
        }
    }
}
