using System.Net.Mime;
using System.Text;
using System.Text.Json;
using PcmFrontendWebUi.Models;

namespace PcmFrontendWebUi.Repositories;

public class ArticleTypeRepository : IRepository<ArticleType, int>
{
    private readonly HttpClient _httpClient;
    private static string _articleTypeEndpoint = $"{Endpoints.BaseUrl}{Endpoints.ArticleType}";


    public ArticleTypeRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ArticleType>> GetAll()
    {
        using var responseMessage = await _httpClient.GetAsync(_articleTypeEndpoint);
        if (responseMessage.IsSuccessStatusCode)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);
            var root = doc.RootElement.GetProperty("articleTypes");
            var articleTypes = root.Deserialize<List<ArticleType>>(new JsonSerializerOptions
                {PropertyNameCaseInsensitive = true});
            return articleTypes;
        }

        return new List<ArticleType>();
    }

    public async Task<ArticleType> Get(int id)
    {
        using var responseMessage = await _httpClient.GetAsync($"{_articleTypeEndpoint}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var articleType = await responseMessage.Content.ReadFromJsonAsync<ArticleType>();
            return articleType;
        }

        return new ArticleType {IsResponseSuccess = false};
    }

    public async Task<bool> Insert(ArticleType entity)
    {
        var json = new StringContent(JsonSerializer.Serialize(entity),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        using var responseMessage = await _httpClient.PostAsync(_articleTypeEndpoint, json);
        return responseMessage.IsSuccessStatusCode;
    }

    public async Task<bool> Delete(int id)
    {
        using var responseMessage = await _httpClient.DeleteAsync($"{_articleTypeEndpoint}/{id}");
        return responseMessage.IsSuccessStatusCode;
    }
}