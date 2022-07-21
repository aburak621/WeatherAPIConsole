using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherAPIConsole
{
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