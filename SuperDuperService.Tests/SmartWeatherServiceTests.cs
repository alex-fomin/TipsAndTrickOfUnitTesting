using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SuperDuperService.Models;
using SuperDuperService.Services;
using Xunit;

namespace SuperDuperService.Tests
{
	public class SmartWeatherServiceTests
	{
		[Fact]
		public async Task GetTemperatureAsync_ShouldUseFirstLocation()
		{
			var locationService = new Mock<ILocationService>();
			locationService.Setup(x => x.GeocodeAsync("Minsk"))
				.ReturnsAsync(new[] { new GeoLocation { Lat = 1, Lon = 1 }, new GeoLocation { Lat = 2, Lon = 2 } });

			var weatherService = new Mock<IWeatherService>();

			weatherService.Setup(x => x.GetWeatherAsync(It.Is<GeoLocation>(l => l.Lat == 1 && l.Lon == 1)))
				.ReturnsAsync(new Weather
				{
					Condition = Condition.Sunny,
					Temperature = 232.8,
					Description = "Hot"
				});

			var service = new SmartWeatherService(Mock.Of<ILogger<SmartWeatherService>>(), weatherService.Object, locationService.Object, Mock.Of<IMemoryCache>());

			var actualWeather = await service.GetRealTimeWeatherAsync("Minsk");

			Assert.Equal(actualWeather.Temperature, 232.8);
			Assert.Equal(actualWeather.Condition, Condition.Sunny);
			Assert.Equal(actualWeather.Description, "Hot");
		}
	}
}