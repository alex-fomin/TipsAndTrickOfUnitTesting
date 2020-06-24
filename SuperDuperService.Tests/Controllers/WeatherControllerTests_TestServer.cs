using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SuperDuperService.Services;
using Xunit;

namespace SuperDuperService.Tests.Controllers
{
	public class WeatherControllerTests_TestServer
	{
		[Fact]
		public async Task GetTest_ShouldReturnTestValues()
		{
			var mock = new Mock<ISmartWeatherService>();
			mock.Setup(x => x.GetRealTimeWeatherAsync("No name"))
				.ReturnsAsync((Weather) null);

			var client = new TestServer(new WebHostBuilder().UseStartup<Startup>()
					.ConfigureServices(sc => { sc.AddSingleton(mock.Object); }))
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