using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using Pcm.Application.Interfaces;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure.Repositories;

//todo: Anpassung für Änderung an Api: loadout nicht unter 'loadouts/{training-id}' sondern unter '/training/{training-id}/loadout'
public class Repository<TResponse, TRequest> : IRepository<TResponse, TRequest>
    where TResponse : ResponseBase, new()
    where TRequest : class, new()
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;
    private readonly string _uri;
    private TResponse _type;


    public Repository(HttpClient httpClient, IEndpointService endpointService)
    {
        var types = GetType().GenericTypeArguments;
        _httpClient = httpClient;
        _uri = endpointService.GetMappedUrl(types[0]);
        _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }


    public async Task<IEnumerable<TResponse>> GetAll(Dictionary<string, string>? queries = default)
    {
        try
        {
            var query = ExtractQuery(queries);
            var response = await _httpClient.GetFromJsonAsync<List<TResponse>>($"{_uri}{query}", _options);
            return response ?? Enumerable.Empty<TResponse>().ToList();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<TResponse> {new() {IsResponseSuccess = false}};
        }
    }

    public async Task<TResponse> Get(int id, Dictionary<string, string>? queries = default)
    {
        try
        {
            var query = ExtractQuery(queries);
            var response = await _httpClient.GetFromJsonAsync<TResponse>($"{_uri}/{id}{query}", _options);
            return response ?? new TResponse();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TResponse
            {
                IsResponseSuccess = false
            };
        }
    }

    public async Task<TResponse> Insert(TRequest requestModel)
    {
        //todo: insert training with corresponding loadout
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_uri, requestModel);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseString);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TResponse
            {
                IsResponseSuccess = false
            };
        }
    }

    public async Task<TResponse> Update(TRequest requestModel, int id)
    {
        //todo: update training with corresponding loadout
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{_uri}/{id}", requestModel);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseString);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TResponse
            {
                IsResponseSuccess = false
            };
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{_uri}/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException e)
        {
            Debug.WriteLine(e);
            return false;
        }
    }

    private string ExtractQuery(Dictionary<string, string>? queries)
    {
        if (queries == null || queries.Count == 0) return "";
        var query = queries.Aggregate("?", (current, kv) => current + $"{kv.Key}={kv.Value}&");
        return query.Remove(query.Length - 1);
    }
}