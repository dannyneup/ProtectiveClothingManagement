using System.Net.Mime;
using System.Text;
using System.Text.Json;
using PcmFrontendWebUi.Models;

namespace PcmFrontendWebUi.Repositories;

public class ApprenticeshipRepository : IRepository<Apprenticeship, int>
{
    private readonly HttpClient _httpClient;
    private readonly string _apprenticeshipEndpoint = $"{Endpoints.BaseUrl}{Endpoints.Apprenticeship}";

    public ApprenticeshipRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Apprenticeship>> GetAll()
    {
        using var responseMessage = await _httpClient.GetAsync(_apprenticeshipEndpoint);
        if (responseMessage.IsSuccessStatusCode)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);
            var root = doc.RootElement.GetProperty("apprenticeships");
            var apprenticeships = root.Deserialize<List<Apprenticeship>>(new JsonSerializerOptions
                {PropertyNameCaseInsensitive = true});
            return apprenticeships;
        }

        return new List<Apprenticeship>();
    }

    public async Task<Apprenticeship> Get(int id)
    {
        using var responseMessage = await _httpClient.GetAsync($"{_apprenticeshipEndpoint}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var apprenticeship = await responseMessage.Content.ReadFromJsonAsync<Apprenticeship>();
            return apprenticeship;
        }

        return new Apprenticeship {IsResponseSuccess = false};
    }

    public async Task<bool> Insert(Apprenticeship entity)
    {
        var json = new StringContent(JsonSerializer.Serialize(entity),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        using var responseMessage = await _httpClient.PostAsync(_apprenticeshipEndpoint, json);
        return responseMessage.IsSuccessStatusCode;
    }


    public async Task<bool> Delete(int id)
    {
        using var responseMessage = await _httpClient.DeleteAsync($"{_apprenticeshipEndpoint}/{id}");
        return responseMessage.IsSuccessStatusCode;
    }
}