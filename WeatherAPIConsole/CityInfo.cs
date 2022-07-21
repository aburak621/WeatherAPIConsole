using System.Text.Json.Serialization;

namespace WeatherAPIConsole
{
    /// <summary>
    /// Holds the weather history data for a city. This data is provided by requesting a json from the weatherapi.com and deserializing the json fields into an instance of this class.
    /// </summary>
    public class CityInfo
    {
        [JsonPropertyName("location")] public Location Location { get; set; }
        [JsonPropertyName("forecast")] public Forecast Forecast { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("country")] public string? Country { get; set; }
    }

    public class Forecast
    {
        [JsonPropertyName("forecastday")] public List<ForecastDay> ForecastDays { get; set; }
    }

    public class ForecastDay
    {
        [JsonPropertyName("day")] public Day Day { get; set; }
        [JsonPropertyName("astro")] public Astro Astro { get; set; }
    }

    public class Day
    {
        [JsonPropertyName("mintemp_c")] public double MinTemp { get; set; }
        [JsonPropertyName("maxtemp_c")] public double MaxTemp { get; set; }
    }

    public class Astro
    {
        [JsonPropertyName("sunrise")] public string Sunrise { get; set; }
        [JsonPropertyName("sunset")] public string Sunset { get; set; }
    }
}