using System.Text.Json;
using RestSharp;

namespace WeatherAPIConsole
{
    /// <summary>
    /// This console application takes city names as command line arguments and outputs a csv file containing recent sunrise, sunset hours and minimum, maximum temperatures.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            // Read the API key from the key.txt file.
            // So the API key is not accessible by strangers from the public repository.
            string apiKey;
            try
            {
                apiKey = File.ReadAllText(Path.Combine(currentDirectory, "key.txt")).Trim();
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Please enter your weatherapi.com key in a text file named key.txt in the same directory as the program.");
                return;
            }

            // Dates used to query the last 3 days.
            DateTime today = DateTime.Today;
            string requestStartDate = today.AddDays(-3).ToString("yyyy-MM-dd");
            string requestEndDate = today.AddDays(-1).ToString("yyyy-MM-dd");


            string outputPath = Path.Combine(currentDirectory, "outputForecast.csv");
            using (StreamWriter writer = new StreamWriter(File.Open(outputPath, FileMode.Create)))
            {
                writer.AutoFlush = true;
                writer.WriteLine("Country,Name,Sunrise,Sunset,Min_Temp_C,Max_Temp_C");

                // Every city is requested from the api and written to the output file.
                foreach (string city in args)
                {
                    string requestUrl =
                        $"http://api.weatherapi.com/v1/history.json?key={apiKey}&dt={requestStartDate}&end_dt={requestEndDate}&q={city}";
                    RestClient client = new RestClient(requestUrl);
                    RestRequest request = new RestRequest();
                    RestResponse response = client.Execute(request);

                    CityInfo? cityInfo = JsonSerializer.Deserialize<CityInfo>(response.Content ?? string.Empty);

                    if (cityInfo is null)
                    {
                        Console.WriteLine($"Weather API failed to response for the city {city}!");
                        continue;
                    }

                    if (cityInfo.Location is null)
                    {
                        Console.WriteLine($"{city} is not a valid city name.");
                        continue;
                    }

                    foreach (ForecastDay day in cityInfo.Forecast.ForecastDays)
                    {
                        writer.WriteLine(
                            $"{cityInfo.Location.Country},{cityInfo.Location.Name},{day.Astro.Sunrise.Replace(" ", "")},{day.Astro.Sunset.Replace(" ", "")},{day.Day.MinTemp},{day.Day.MaxTemp}");
                    }
                }
            }
        }
    }
}