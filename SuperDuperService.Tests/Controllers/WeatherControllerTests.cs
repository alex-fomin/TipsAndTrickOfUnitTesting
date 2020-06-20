using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.AutoMock;
using SuperDuperService.Controllers;
using SuperDuperService.Services;
using Xunit;

namespace SuperDuperService.Tests.Controllers
{
	public class WeatherControllerTests
	{
		[Fact]
		public async Task Get_Should_ReturnNotFound_IfLocationNotFound()
		{
			var mock = new AutoMocker(MockBehavior.Strict);
			mock.Use<ILogger<WeatherController>>(new NullLogger<WeatherController>());

			mock.GetMock<ISmartWeatherService>()
				.Setup(x => x.GetRealTimeWeatherAsync("No name"))
				.ReturnsAsync((Weather) null);

			var controller = mock.CreateInstance<WeatherController>();

			(await controller.Get("No name")).Should().BeOfType<NotFoundResult>();
		}
	}
}