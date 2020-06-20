using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using SuperDuperService.Models;
using SuperDuperService.Services;
using SuperDuperService.Services.LocationIQ;
using Xunit;

namespace SuperDuperService.Tests.Integration
{

	public class LocationIQServiceTests
	{
		[Fact]
		public async Task GetMinskLocation()
		{
			var service = GetLocationService();

			var minskGeoLocations = await service.GeocodeAsync("Minsk");

			var geoLocation = minskGeoLocations.First();

			Assert.Equal(53.902334, geoLocation.Lat);
			Assert.Equal(27.5618791, geoLocation.Lon);

			minskGeoLocations.First().Should().BeEquivalentTo(new
			{
				Lat = 53.902334,
				Lon = 27.5618791
			});
		}

		[Fact]
		public async Task GetMinskName()
		{
			var service = GetLocationService();

			var name = await service.ReverseGeocodeAsync(new GeoLocation { Lat = 53.90, Lon = 27.56 });
			name.Should().Be("Minsk");
		}

		private static ILocationService GetLocationService()
		{
			var service = new LocationIQService(new HttpClient()
			{
				BaseAddress = new Uri("https://eu1.locationiq.com/")
			}, Options.Create(new LocationIQServiceSettings
			{
				ApiKey = "d7e04fe76c24e9"
			}));
			return service;
		}
	}
}