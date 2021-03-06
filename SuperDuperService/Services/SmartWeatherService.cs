using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace SuperDuperService.Services
{
	internal class SmartWeatherService : ISmartWeatherService
	{
		private readonly ILogger<SmartWeatherService> _logger;
		private readonly IWeatherService _weatherService;
		private readonly ILocationService _locationService;
		private readonly IMemoryCache _memoryCache;

		public SmartWeatherService(ILogger<SmartWeatherService> logger,
			IWeatherService weatherService,
			ILocationService locationService,
			IMemoryCache memoryCache)
		{
			_logger = logger;
			_weatherService = weatherService;
			_locationService = locationService;
			_memoryCache = memoryCache;
		}

		public async Task<Weather> GetRealTimeWeatherAsync(string locationName)
		{
			var key = $"Location_{locationName}";
			var geoLocation = await _memoryCache.GetOrCreateAsync(key, async entry =>
				{
					entry.SlidingExpiration = TimeSpan.FromDays(1);

					var geoLocations = await _locationService.GeocodeAsync(locationName);
					_logger.LogDebug("Got {Count} locations", geoLocations.Length);


					return geoLocations.FirstOrDefault();
				}
			);

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

		public async Task<Weather> GetWeatherAsync(string locationName)
		{
			return await _memoryCache.GetOrCreateAsync(locationName, async cacheEntry =>
			{
				var result = await GetRealTimeWeatherAsync(locationName);
				cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
				return result;
			});
		}
	}
}