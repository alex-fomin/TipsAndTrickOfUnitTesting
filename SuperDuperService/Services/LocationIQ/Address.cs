using Newtonsoft.Json;

namespace SuperDuperService.Services.LocationIQ
{
	public partial class Address
	{
		[JsonProperty("cafe")] public string Cafe { get; set; }

		[JsonProperty("road")] public string Road { get; set; }

		[JsonProperty("suburb")] public string Suburb { get; set; }
		[JsonProperty("city")] public string City { get; set; }

		[JsonProperty("county")] public string County { get; set; }

		[JsonProperty("region")] public string Region { get; set; }

		[JsonProperty("state")] public string State { get; set; }

		[JsonProperty("postcode")] public string Postcode { get; set; }

		[JsonProperty("country")] public string Country { get; set; }

		[JsonProperty("country_code")] public string CountryCode { get; set; }
	}
}