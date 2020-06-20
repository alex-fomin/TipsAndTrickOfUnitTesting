using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SuperDuperService.Models;

namespace SuperDuperService.Services
{
	internal class SmartWeatherService : ISmartWeatherService
	{
		private readonly ILogger<SmartWeatherService> _logger;
		private readonly IWeatherService _weatherService;
		private readonly ILocationService _locationService;

		public SmartWeatherService(ILogger<SmartWeatherService> logger,
			IWeatherService weatherService,
			ILocationService locationService)
		{
			_logger = logger;
			_weatherService = weatherService;
			_locationService = locationService;
		}

		public async Task<Weather> GetRealTimeWeatherAsync(string locationName)
		{
			_logger.LogDebug("Get temperature for {Name}", locationName);

			var geoLocations = await _locationService.GeocodeAsync(locationName);
			_logger.LogDebug("Got {Count} locations", geoLocations.Length);

			var geoLocation = geoLocations.FirstOrDefault();

			if (geoLocation != null)
			{
				_logger.LogInformation("Use {Lat}:{Lon} for {Name}", geoLocation.Lat, geoLocation.Lon, locationName);

				return await _weatherService.GetWeatherAsync(geoLocation);
			}
			else
			{
				return null;
			}
		}
	}
}