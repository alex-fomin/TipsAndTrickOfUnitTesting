using System.Threading.Tasks;

namespace SuperDuperService.Services
{
	public interface ISmartWeatherService
	{
		Task<Weather> GetRealTimeWeatherAsync(string locationName);
	}
}