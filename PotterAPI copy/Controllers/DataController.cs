using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
[Route("api/data")]
public class DataController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public DataController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetData()
    {
        var url = "https://potterapi-fedeperin.vercel.app/"; // URL da API externa

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Garante que a requisição foi bem-sucedida

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<dynamic>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Ok(data);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Erro ao buscar dados da API externa: {ex.Message}");
        }
    }
}
