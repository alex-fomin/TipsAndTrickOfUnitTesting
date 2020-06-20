using Newtonsoft.Json;

namespace SuperDuperService.Services.LocationIQ
{
	public partial class Location
	{
		[JsonProperty("place_id")] public long PlaceId { get; set; }

		[JsonProperty("licence")] public string Licence { get; set; }

		[JsonProperty("osm_type")] public string OsmType { get; set; }

		[JsonProperty("osm_id")] public string OsmId { get; set; }

		[JsonProperty("lat")] public string Lat { get; set; }

		[JsonProperty("lon")] public string Lon { get; set; }

		[JsonProperty("display_name")] public string DisplayName { get; set; }

		[JsonProperty("address")] public Address Address { get; set; }

		[JsonProperty("boundingbox")] public string[] BoundingBox { get; set; }
	}
}