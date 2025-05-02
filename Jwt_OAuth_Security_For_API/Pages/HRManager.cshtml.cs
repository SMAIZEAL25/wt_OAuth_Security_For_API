using Jwt_OAuth_Security_For_API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            WeatherForeCastItems = await  httpclient.GetFromJsonAsync<List<WeatherForeCastDTO>>("WeatherForecast") ?? new List<WeatherForeCastDTO>();
        }
    }
}
