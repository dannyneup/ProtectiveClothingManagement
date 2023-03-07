using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using Pcm.Application.Interfaces;
using Pcm.Application.Interfaces.RequestModels;
using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure.Repositories;

public class TrainingInfoRepository : IRepository<ITrainingInfoResponseModel, ITrainingInfoRequestModel>
{
    private readonly string _trainingInfoRequestUri = $"{Endpoints.BaseUrl}{Endpoints.TrainingInfo}";
    private readonly HttpClient _httpClient;

    public TrainingInfoRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ITrainingInfoResponseModel>> GetAll()
    {
        try
        {
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return await _httpClient.GetFromJsonAsync<List<TrainingInfoResponseModel>>(_trainingInfoRequestUri, options);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<TrainingInfoResponseModel>();
        }
    }

    public async Task<ITrainingInfoResponseModel> Get(int id)
    {
        try
        {
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return await _httpClient.GetFromJsonAsync<TrainingInfoResponseModel>($"{_trainingInfoRequestUri}/{id}",
                options);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TrainingInfoResponseModel();
        }
    }

    public async Task<ITrainingInfoResponseModel> Insert(ITrainingInfoRequestModel requestModel)
    {
        throw new NotImplementedException();
    }

    public Task<ITrainingInfoResponseModel> Update(ITrainingInfoResponseModel responseModel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}