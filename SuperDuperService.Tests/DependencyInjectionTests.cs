using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SuperDuperService.Tests
{
	public class DependencyInjectionTests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly IServiceProvider _services;

		public DependencyInjectionTests(WebApplicationFactory<Startup> factory)
		{
			_services = factory.Services;
		}

		[Theory]
		[MemberData(nameof(AllControllers))]
		public void AllControllersHaveAllDependenciesRegistered(Type controllerType)
		{
			ActivatorUtilities.GetServiceOrCreateInstance(_services, controllerType)
				.Should().NotBeNull();
		}

		public static IEnumerable<object[]> AllControllers()
		{
			return typeof(Startup).Assembly.GetTypes()
				.Where(x => !x.IsAbstract && typeof(ControllerBase).IsAssignableFrom(x))
				.Select(x => new object[] { x });
		}
	}
}