using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using Pcm.Application.Interfaces;

namespace Pcm.Infrastructure.Repositories;

public class Repository<TResponse, TRequest> : IRepository<TResponse, TRequest> 
    where TResponse : class, new() 
    where TRequest : class, new()
{
    private readonly string _uri;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;
    private TResponse _type;


    public Repository(HttpClient httpClient, IEndpointService endpointService)
    {
        var types = GetType().GenericTypeArguments;
        _httpClient = httpClient;
        _uri = endpointService.GetMappedUrl(types[0]);
        _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }

    public async Task<IEnumerable<TResponse>> GetAll(Dictionary<string, string>? querys = default)
    {
        try
        {
            string query = ExtractQuery(querys);
            var response = await _httpClient.GetFromJsonAsync<List<TResponse>>($"{_uri}{query}", _options);
            return response ?? Enumerable.Empty<TResponse>().ToList();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return Enumerable.Empty<TResponse>().ToList();
        }
    }

    public async Task<TResponse> Get(int id, Dictionary<string, string>? querys = default)
    {
        try
        {
            string query = ExtractQuery(querys);
            var response = await _httpClient.GetFromJsonAsync<TResponse>($"{_uri}/{id}{query}", _options);
            return response ?? new TResponse();
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new TResponse();
        }
    }

    public async Task<TResponse> Insert(TRequest requestModel)
    {
        return new TResponse();
    }

    public async Task<TResponse> Update(TResponse responseModel)
    {
        return new TResponse();
    }

    public async Task<bool> Delete(int id)
    {
        return true;
    }

    private string ExtractQuery(Dictionary<string, string>? queries)
    {
        if (queries == null || queries.Count == 0)
        {
            return "";
        }
        string query = queries.Aggregate("?", (current, kv) => current + $"{kv.Key}={kv.Value}&");
        return query.Remove(query.Length - 1);
    }
}