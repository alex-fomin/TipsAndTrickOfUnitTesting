using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SuperDuperService.Controllers;
using SuperDuperService.Services;
using SuperDuperService.Services.LocationIQ;
using SuperDuperService.Services.OpenWeatherMapService;

namespace SuperDuperService
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();


			services.AddTransient<ISmartWeatherService, SmartWeatherService>();

			services.Configure<LocationIQServiceSettings>(Configuration.GetSection("locationIq"));
			services.AddHttpClient<ILocationService, LocationIQService>();
			services.Configure<OpenWeatherMapServiceSettings>(Configuration.GetSection("OpenWeatherMap"));
			services.AddHttpClient<IWeatherService, OpenWeatherMapService>();

			services.AddMemoryCache();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}