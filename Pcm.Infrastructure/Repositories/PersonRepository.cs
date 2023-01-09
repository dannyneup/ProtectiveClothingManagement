using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.Infrastructure.Repositories;

public class PersonRepository : IRepository<IPerson, int>
{
    private readonly HttpClient _httpClient;
    private readonly string _personEndpoint = $"{Endpoints.BaseUrl}{Endpoints.Person}";

    public PersonRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<IPerson>> GetAll()
    {
        using var responseMessage = await _httpClient.GetAsync(_personEndpoint);
        if (responseMessage.IsSuccessStatusCode)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);
            var root = doc.RootElement.GetProperty("persons");
            var persons = root.Deserialize<List<Person>>(new JsonSerializerOptions
                {PropertyNameCaseInsensitive = true});
            return persons;
        }

        return new List<Person>();
    }

    public async Task<IPerson> Get(int id)
    {
        using var responseMessage = await _httpClient.GetAsync($"{_personEndpoint}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var person = await responseMessage.Content.ReadFromJsonAsync<Person>();
            return person;
        }

        return new Person {IsResponseSuccess = false};
    }

    public async Task<bool> Insert(IPerson entity)
    {
        var json = new StringContent(JsonSerializer.Serialize(entity),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        using var responseMessage = await _httpClient.PostAsync(_personEndpoint, json);
        return responseMessage.IsSuccessStatusCode;
    }


    public async Task<bool> Delete(int id)
    {
        using var responseMessage = await _httpClient.DeleteAsync($"{_personEndpoint}/{id}");
        return responseMessage.IsSuccessStatusCode;
    }
}