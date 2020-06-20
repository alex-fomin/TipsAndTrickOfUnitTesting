using Newtonsoft.Json;

namespace SuperDuperService.Services.OpenWeatherMapService.Models
{
	public partial class Weather
	{
		[JsonProperty("lat")] public double Lat { get; set; }

		[JsonProperty("lon")] public double Lon { get; set; }

		[JsonProperty("timezone")] public string Timezone { get; set; }

		[JsonProperty("timezone_offset")] public long TimezoneOffset { get; set; }

		[JsonProperty("current")] public Current Current { get; set; }

		[JsonProperty("id")] public long Id { get; set; }

		[JsonProperty("main")] public string Main { get; set; }

		[JsonProperty("description")] public string Description { get; set; }

		[JsonProperty("icon")] public string Icon { get; set; }
	}
}