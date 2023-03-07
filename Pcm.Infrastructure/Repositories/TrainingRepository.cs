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

public class TrainingRepository : ITrainingRepository
{
    private readonly string _trainingRequestUri = $"{Endpoints.BaseUrl}{Endpoints.Training}";
    private readonly HttpClient _httpClient;

    public TrainingRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ITrainingResponseModel>> GetAll()
    {
        try
        {
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return await _httpClient.GetFromJsonAsync<List<TrainingResponseModel>>(_trainingRequestUri, options);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<TrainingResponseModel>();
        }
    }

    public async Task<ITrainingResponseModel> Get(int id)
    {
        try
        {
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return await _httpClient.GetFromJsonAsync<TrainingResponseModel>($"{_trainingRequestUri}/{id}",
                options);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TrainingResponseModel();
        }
    }

    public async Task<IEnumerable<IPersonInfoResponseModel>> GetTrainees(int id)
    {
        try
        {
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return await _httpClient.GetFromJsonAsync<List<PersonInfoResponseModel>>($"{_trainingRequestUri}/{id}{Endpoints.PersonInfos}",
                options);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<IPersonInfoResponseModel>();
        }
    }

    public async Task<IEnumerable<ILoadOutPartResponseModel>> GetLoadOut(int id)
    {
        try
        {
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return await _httpClient.GetFromJsonAsync<List<LoadOutPartResponseModel>>($"{_trainingRequestUri}/{id}{Endpoints.LoadOut}",
                options);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<LoadOutPartResponseModel>();
        }
    }

    public async Task<ITrainingResponseModel> Insert(ITrainingRequestModel requestModel)
    {
        var response = await _httpClient.PostAsJsonAsync(_trainingRequestUri, requestModel);
        if (!response.IsSuccessStatusCode)
        {
            return new TrainingResponseModel() {IsResponseSuccess = false};
        }
        var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
        return await response.Content.ReadFromJsonAsync<TrainingResponseModel>() ?? new TrainingResponseModel();
    }

    public async Task<IEnumerable<ILoadOutPartResponseModel>> InsertLoadOut(int trainingId,
        IEnumerable<ILoadOutPartRequestModel> loadOut)
    {
        var requestUri = $"{_trainingRequestUri}/{trainingId}{Endpoints.LoadOut}";
        var response = await _httpClient.PostAsJsonAsync(requestUri, loadOut);
        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
            return (await response.Content.ReadFromJsonAsync<List<LoadOutPartResponseModel>>())!;
        }
        return new List<LoadOutPartResponseModel>()
        {
            new()
            {
                IsResponseSuccess = false
            }
        };
    }

    public async Task<ITrainingResponseModel> Update(ITrainingResponseModel responseModel)
    {
        var requestUri = $"{_trainingRequestUri}/{responseModel.Id}";
        var response = await _httpClient.PutAsJsonAsync(requestUri, responseModel);
        if (!response.IsSuccessStatusCode)
        {
            return new TrainingResponseModel() {IsResponseSuccess = false};
        }

        return await response.Content.ReadFromJsonAsync<TrainingResponseModel>() ?? new TrainingResponseModel();
    }

    public async Task<bool> Delete(int id)
    {
        var requestUri = $"{_trainingRequestUri}/{id}";
        var response = await _httpClient.DeleteAsync(requestUri);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }
}