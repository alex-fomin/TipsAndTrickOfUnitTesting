using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SuperDuperService.Models;
using WeatherResponse=SuperDuperService.Services.OpenWeatherMapService.Models.Weather;

namespace SuperDuperService.Services.OpenWeatherMapService
{
	class OpenWeatherMapService : IWeatherService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public OpenWeatherMapService(HttpClient httpClient, IOptions<OpenWeatherMapServiceSettings> settings)
		{
			_httpClient = httpClient;
			_apiKey = settings.Value.ApiKey;
		}

		public async Task<double> GetTemperatureAsync(GeoLocation location)
		{
			var responseMessage = await _httpClient.GetAsync(
				$"https://api.openweathermap.org/data/2.5/onecall?lat={location.Lat}&lon={location.Lon}&exclude=hourly,daily&units=metric&appid={_apiKey}");

			var response = await responseMessage.Content.ReadAsAsync<WeatherResponse>();

			return response.Current.Temp;
		}

		public async Task<Weather> GetWeatherAsync(GeoLocation location)
		{
			var responseMessage = await _httpClient.GetAsync(
				$"https://api.openweathermap.org/data/2.5/onecall?lat={location.Lat}&lon={location.Lon}&exclude=hourly,daily&units=metric&appid={_apiKey}");
			var response = await responseMessage.Content.ReadAsAsync<WeatherResponse>();

			return new Weather
			{
				Temperature = response.Current.Temp,
				Condition = Map(response.Description),
				Description = response.Main
			};
		}

		private static Condition Map(string description) =>
			description switch
			{
				"Rain" => Condition.Rainy,
				"Snow" => Condition.Snowy,
				_ => Condition.Sunny
			};
	}
}