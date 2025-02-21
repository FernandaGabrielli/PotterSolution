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

        public DataController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet("characters")]
        public async Task<IActionResult> GetCharacters()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://potterapi-fedeperin.vercel.app/en/characters");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, $"Erro ao buscar dados da API externa: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<dynamic>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar dados: {ex.Message}");
            }
        }
    }
}
