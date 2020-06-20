using System.Threading.Tasks;
using SuperDuperService.Models;

namespace SuperDuperService.Services
{
	public interface IWeatherService
	{
		Task<double> GetTemperatureAsync(GeoLocation location);

		Task<Weather> GetWeatherAsync(GeoLocation location);
	}
}