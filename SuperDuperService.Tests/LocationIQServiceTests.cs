using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using SuperDuperService.Models;
using SuperDuperService.Services.LocationIQ;
using Xunit;

namespace SuperDuperService.Tests
{
	public class LocationIqServiceTests
	{
		[Fact]
		public async Task ReverseGeocodeAsync_ShouldReturnRegion_IfThereIsNoCity()
		{
			var mocker = new AutoMocker(MockBehavior.Strict);

			mocker.Use(Options.Create(new LocationIQServiceSettings { ApiKey = "apiKey" }));

			var service = mocker.CreateInstance<LocationIQService>();

			var result = await service.ReverseGeocodeAsync(new GeoLocation
			{
				Lat = 54,
				Lon = 27
			});

			result.Should().Be("Minsk Region");
		}
	}
}