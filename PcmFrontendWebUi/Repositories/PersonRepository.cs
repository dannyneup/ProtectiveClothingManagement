using System.Net.Mime;
using System.Text;
using System.Text.Json;
using PcmFrontendWebUi.Models;

namespace PcmFrontendWebUi.Repositories;

public class PersonRepository : IRepository<Person, int>
{
    private readonly HttpClient _httpClient;
    private readonly string _personEndpoint = $"{Endpoints.BaseUrl}{Endpoints.Person}";

    public PersonRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Person>> GetAll()
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

    public async Task<Person> Get(int id)
    {
        using var responseMessage = await _httpClient.GetAsync($"{_personEndpoint}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var person = await responseMessage.Content.ReadFromJsonAsync<Person>();
            return person;
        }

        return new Person {IsResponseSuccess = false};
    }

    public async Task<bool> Insert(Person entity)
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