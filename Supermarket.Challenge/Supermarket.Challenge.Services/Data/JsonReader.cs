using Newtonsoft.Json;
using Supermarket.Challenge.Domain.Config;

namespace Supermarket.Challenge.Services.Data
{
    public class JsonReader : IJsonReader
    {
        public static DataConfig DataFromJson { get; set; }

        public void ReadDataFromJson(string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    var jsonData = File.ReadAllText(path);
                    DataFromJson = JsonConvert.DeserializeObject<DataConfig>(jsonData);
                    if (DataFromJson?.Products?.Count == 0)
                    {
                        Console.WriteLine($"Error: File with path {path} is empty.");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: File with path {path} not found.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: File not found: {path}");
            }
            catch (JsonException)
            {
                Console.WriteLine("Error: Invalid JSON format.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: An error occurred: {ex.Message}");
            }
        }
    }
}
