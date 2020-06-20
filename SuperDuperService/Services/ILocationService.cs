using System.Threading.Tasks;
using SuperDuperService.Models;

namespace SuperDuperService.Services
{
	public interface ILocationService
	{
		Task<GeoLocation[]> GeocodeAsync(string name);

		Task<string> ReverseGeocodeAsync(GeoLocation location);
	}
}