using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.AutoMock;
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
			var mocker = new AutoMocker();

			mocker.GetMock<ILocationService>()
				.Setup(x => x.GeocodeAsync("Minsk"))
				.ReturnsAsync(new[] { new GeoLocation { Lat = 1, Lon = 1 }, new GeoLocation { Lat = 2, Lon = 2 } });

			mocker.GetMock<IWeatherService>().Setup(x => x.GetWeatherAsync(It.Is<GeoLocation>(l => l.Lat == 1 && l.Lon == 1)))
				.ReturnsAsync(new Weather
				{
					Condition = Condition.Sunny,
					Temperature = 232.8,
					Description = "Hot"
				});

			var service = mocker.CreateInstance<SmartWeatherService>();

			var actualWeather = await service.GetRealTimeWeatherAsync("Minsk");

			Assert.Equal(actualWeather.Temperature, 232.8);
			Assert.Equal(actualWeather.Condition, Condition.Sunny);
			Assert.Equal(actualWeather.Description, "Hot");
		}
	}
}