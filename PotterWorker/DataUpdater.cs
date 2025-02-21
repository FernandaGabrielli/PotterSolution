using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class DataUpdater
{
    private readonly ILogger<DataUpdater> _logger;
    private readonly HttpClient _httpClient;

    public DataUpdater(ILogger<DataUpdater> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task UpdateCharacters()
    {
        _logger.LogInformation("Buscando personagens da API...");
        var response = await _httpClient.GetStringAsync("https://potterapi-fedeperin.vercel.app/en/characters");
        _logger.LogInformation("Dados recebidos: " + response);
    }
}
