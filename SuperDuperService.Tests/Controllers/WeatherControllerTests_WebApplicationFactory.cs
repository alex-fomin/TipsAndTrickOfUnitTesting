using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SuperDuperService.Services;
using Xunit;

namespace SuperDuperService.Tests.Controllers
{
	public class WeatherControllerTests_WebApplicationFactory : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly WebApplicationFactory<Startup> _factory;

		public WeatherControllerTests_WebApplicationFactory(WebApplicationFactory<Startup> factory)
		{
			_factory = factory;
		}

		[Fact]
		public async Task GetTest_ShouldReturnTestValues()
		{
			var mock = new Mock<ISmartWeatherService>();
			mock.Setup(x => x.GetRealTimeWeatherAsync("No name"))
				.ReturnsAsync((Weather) null);

			var client = _factory.WithWebHostBuilder(b => b.ConfigureServices(sc =>
				{
					sc.AddSingleton(mock.Object);
				}))
				.CreateClient();

			var responseMessage = await client.GetAsync("/weather/test?name=test");

			var response = await responseMessage.Content.ReadAsAnonAsync(new { Name = default(string) });

			response.Should().BeEquivalentTo(new
			{
				Name = "test"
			});
		}



	}
}