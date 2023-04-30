using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using Pcm.Application.Interfaces;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure.Repositories;

public class TrainingRepository : Repository<TrainingResponse, TrainingRequest>, ITrainingRepository<TrainingResponse, TrainingRequest, LoadOutPartResponse>
{
    
    public TrainingRepository(HttpClient httpClient, IEndpointService endpointService) : base(httpClient, endpointService) 
    {
    }
    public async Task<IEnumerable<LoadOutPartResponse>> GetLoadOut(int trainingId)
    {
        try
        {
            var requestUri = $"{Uri}/{trainingId}/{ResourceUrls.LoadOut}";
            var response = await HttpClient.GetFromJsonAsync<List<LoadOutPartResponse>>(requestUri, Options);
            return response ?? Enumerable.Empty<LoadOutPartResponse>().ToList();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e); 
            return new List<LoadOutPartResponse> {new() {IsResponseSuccess = false}};
        }
    }
}