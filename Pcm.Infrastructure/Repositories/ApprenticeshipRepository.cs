using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.Infrastructure.Repositories;

public class ApprenticeshipRepository : IRepository<IApprenticeship, int>
{
    private readonly string _apprenticeshipEndpoint = $"{Endpoints.BaseUrl}{Endpoints.Apprenticeship}";
    private readonly HttpClient _httpClient;

    public ApprenticeshipRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<IApprenticeship>> GetAll()
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

        return new List<IApprenticeship>();
    }

    public async Task<IApprenticeship> Get(int id)
    {
        using var responseMessage = await _httpClient.GetAsync($"{_apprenticeshipEndpoint}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var apprenticeship = await responseMessage.Content.ReadFromJsonAsync<Apprenticeship>();
            return apprenticeship;
        }

        return new Apprenticeship {IsResponseSuccess = false};
    }

    public async Task<bool> Insert(IApprenticeship entity)
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