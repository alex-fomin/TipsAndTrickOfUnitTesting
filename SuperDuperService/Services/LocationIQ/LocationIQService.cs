using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SuperDuperService.Models;

namespace SuperDuperService.Services.LocationIQ
{
	public class LocationIQService : ILocationService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public LocationIQService(HttpClient httpClient, IOptions<LocationIQServiceSettings> settings)
		{
			_httpClient = httpClient;
			_apiKey = settings.Value.ApiKey;
		}


		public async Task<GeoLocation[]> GeocodeAsync(string name)
		{
			var responseMessage = await _httpClient.GetAsync($"https://eu1.locationiq.com//v1/search.php?key={_apiKey}&q={Uri.EscapeUriString(name)}&format=json");

			var response = await responseMessage.Content.ReadAsAsync<GeoLocation[]>();
			return response;

		}

		public async Task<string> ReverseGeocodeAsync(GeoLocation location)
		{
			var responseMessage = await _httpClient.GetAsync($"https://eu1.locationiq.com//v1/reverse.php?key={_apiKey}&lat={location.Lat}&lon={location.Lon}&format=json");

			var response = await responseMessage.Content.ReadAsAsync<Location>();
			return response.Address.City ?? response.Address.Region;
		}
	}
}

