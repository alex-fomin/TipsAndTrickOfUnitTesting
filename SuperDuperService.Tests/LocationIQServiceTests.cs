using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using RichardSzalay.MockHttp;
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

			var mockHttp = new MockHttpMessageHandler();

			mockHttp.When("https://eu1.locationiq.com//v1/reverse.php?key=apiKey&lat=54&lon=27&format=json")
				.Respond("application/json", @"{ ""address"": {  ""region"": ""Minsk Region""  } }");

			mocker.Use(mockHttp.ToHttpClient());

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