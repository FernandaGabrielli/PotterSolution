using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PotterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        // Constructor initializing HttpClient
        public DataController()
        {
            _httpClient = new HttpClient();
        }

        // Endpoint to get all characters
        [HttpGet("characters")]
        public async Task<IActionResult> GetCharacters()
        {
            try
            {
                // Sending a GET request to the external API
                var response = await _httpClient.GetAsync("https://potterapi-fedeperin.vercel.app/en/characters");

                // Checking if the response is successful
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, $"Error fetching data from external API: {response.StatusCode}");
                }

                // Reading the response content as a string
                var json = await response.Content.ReadAsStringAsync();

                // Deserializing the JSON string into a list of dynamic objects
                var data = JsonSerializer.Deserialize<List<dynamic>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return Ok(data); // Returning the list of characters as JSON
            }
            catch (System.Exception ex)
            {
                // Handling any exceptions and returning a 500 error response
                return StatusCode(500, $"Error fetching data: {ex.Message}");
            }
        }

        // Endpoint to get a random character
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomCharacter()
        {
            try
            {
                // Sending a GET request to the external API
                var response = await _httpClient.GetAsync("https://potterapi-fedeperin.vercel.app/en/characters");

                // Checking if the response is successful
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, $"Error fetching data from external API: {response.StatusCode}");
                }

                // Reading the response content as a string
                var json = await response.Content.ReadAsStringAsync();
                
                // Deserialize the JSON string into an array of characters
                var characters = JsonSerializer.Deserialize<dynamic[]>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Check if there are any characters available
                if (characters == null || characters.Length == 0)
                {
                    return NotFound("No characters found.");
                }

                // Selecting a random character from the array
                var random = new Random();
                var randomCharacter = characters[random.Next(characters.Length)];

                return Ok(randomCharacter); // Returning the random character as JSON
            }
            catch (System.Exception ex)
            {
                // Handling any exceptions and returning a 500 error response
                return StatusCode(500, $"Error fetching data: {ex.Message}");
            }
        }
    }
}
