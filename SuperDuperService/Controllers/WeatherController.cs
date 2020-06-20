using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperDuperService.Services;

namespace SuperDuperService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherController : ControllerBase
	{
		private readonly ILogger<WeatherController> _logger;
		private readonly ISmartWeatherService _smartWeatherService;

		public WeatherController(ILogger<WeatherController> logger, ISmartWeatherService smartWeatherService)
		{
			_logger = logger;
			_smartWeatherService = smartWeatherService;
		}

		[HttpGet]
		public async Task<ActionResult> Get(string name)
		{
			_logger.LogInformation("Get temperature for {Name}", name);
			var weather = await _smartWeatherService.GetRealTimeWeatherAsync(name);
			if (weather == null)
			{
				return NotFound();
			}
			return Ok(new
			{
				Name = name,
				weather.Temperature,
				weather.Condition,
				Main = weather.Description
			});
		}

		[HttpGet("test")]
		public async Task<ActionResult> GetTest(string name)
		{
			if (name == "test")
			{
				return Ok(new
				{
					Name = "test"
				});
			}

			return await Get(name);
		}
	}
}