using Newtonsoft.Json;

namespace SuperDuperService.Services.OpenWeatherMapService.Models
{
	public partial class Current
	{
		[JsonProperty("dt")] public long Dt { get; set; }

		[JsonProperty("sunrise")] public long Sunrise { get; set; }

		[JsonProperty("sunset")] public long Sunset { get; set; }

		[JsonProperty("temp")] public double Temp { get; set; }

		[JsonProperty("feels_like")] public double FeelsLike { get; set; }

		[JsonProperty("pressure")] public long Pressure { get; set; }

		[JsonProperty("humidity")] public long Humidity { get; set; }

		[JsonProperty("dew_point")] public double DewPoint { get; set; }

		[JsonProperty("uvi")] public double Uvi { get; set; }

		[JsonProperty("clouds")] public long Clouds { get; set; }

		[JsonProperty("visibility")] public long Visibility { get; set; }

		[JsonProperty("wind_speed")] public long WindSpeed { get; set; }

		[JsonProperty("wind_deg")] public long WindDeg { get; set; }

		[JsonProperty("wind_gust")] public long WindGust { get; set; }

		[JsonProperty("weather")] public Weather[] Weather { get; set; }

	}
}