using System.Net.Mime;
using System.Text;
using System.Text.Json;
using PcmFrontendWebUi.Models;

namespace PcmFrontendWebUi.Repositories;

public class ArticleRepository : IRepository<Article, int>
{
    private readonly HttpClient _httpClient;
    private static string _articleEndpoint = $"{Endpoints.BaseUrl}{Endpoints.Article}";


    public ArticleRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Article>> GetAll()
    {
        using var responseMessage = await _httpClient.GetAsync($"{_articleEndpoint}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);
            var root = doc.RootElement.GetProperty("articles");
            var articles = root.Deserialize<List<Article>>(new JsonSerializerOptions
                {PropertyNameCaseInsensitive = true});
            return articles;
        }

        return new List<Article>();
    }

    public async Task<Article> Get(int id)
    {
        using var responseMessage = await _httpClient.GetAsync($"{_articleEndpoint}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var article = await responseMessage.Content.ReadFromJsonAsync<Article>();
            return article;
        }

        return new Article {IsResponseSuccess = false};
    }

    public async Task<bool> Insert(Article entity)
    {
        var json = new StringContent(JsonSerializer.Serialize(entity),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        using var responseMessage = await _httpClient.PostAsync(_articleEndpoint, json);
        return responseMessage.IsSuccessStatusCode;
    }


    public async Task<bool> Delete(int id)
    {
        using var responseMessage = await _httpClient.DeleteAsync($"{_articleEndpoint}/{id}");
        return responseMessage.IsSuccessStatusCode;
    }
}