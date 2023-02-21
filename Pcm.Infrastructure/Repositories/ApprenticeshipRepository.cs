using System.Diagnostics;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;
using Pcm.Infrastructure.ResponseModels;

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
        try
        {
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return await _httpClient.GetFromJsonAsync<List<ApprenticeshipResponseModel>>(_apprenticeshipEndpoint, options);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<ApprenticeshipResponseModel>();
        }
    }

    public async Task<IApprenticeship> Get(int id)
    {
        using var responseMessage = await _httpClient.GetAsync($"{_apprenticeshipEndpoint}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var apprenticeship = await responseMessage.Content.ReadFromJsonAsync<ApprenticeshipResponseModel>();
            return apprenticeship;
        }

        return new ApprenticeshipResponseModel {IsResponseSuccess = false};
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