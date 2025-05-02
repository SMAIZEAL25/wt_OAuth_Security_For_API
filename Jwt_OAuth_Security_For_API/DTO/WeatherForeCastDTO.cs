namespace Jwt_OAuth_Security_For_API.DTO
{
    public class WeatherForeCastDTO
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string? Summary { get; set; }
    }
}
