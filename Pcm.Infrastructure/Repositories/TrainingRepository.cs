using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using Pcm.Application.Interfaces;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Application.Interfaces.RequestModels;
using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure.Repositories;

public class TrainingRepository : Repository<TrainingResponse, TrainingRequest>, ITrainingRepository<TrainingResponse, TrainingRequest>
{
    
    public TrainingRepository(HttpClient httpClient, IEndpointService endpointService) : base(httpClient, endpointService) 
    {
    }

    public async Task<ILoadOutPartResponse> InsertLoadOut(int trainingId, IEnumerable<ILoadOutPartRequest> requestModel)
    {
        try
        {
            var requestUri = $"{_uri}/{trainingId}/{ResourceUrls.LoadOut}";
            var response = await _httpClient.PostAsJsonAsync(requestUri, requestModel);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LoadOutPartResponse>(responseString);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new LoadOutPartResponse()
            {
                IsResponseSuccess = false
            };
        }
    }
    public async Task<IEnumerable<ILoadOutPartResponse>> GetLoadOut(int trainingId)
    {
        try
        {
            var requestUri = $"{_uri}/{trainingId}/{ResourceUrls.LoadOut}";
            var response = await _httpClient.GetFromJsonAsync<List<LoadOutPartResponse>>(requestUri, _options);
            return response ?? Enumerable.Empty<LoadOutPartResponse>().ToList();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e); 
            return new List<LoadOutPartResponse> {new() {IsResponseSuccess = false}};
        }
    }
}