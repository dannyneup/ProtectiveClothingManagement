using System.Text.Json;
using PcmFrontendWebUi.Models;

namespace PcmFrontendWebUi.Repositories;

public class ArticleRepository : IRepository<Article, int>
{
    private readonly HttpClient _httpClient;


    public ArticleRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Article>> GetAll()
    {
        using var responseMessage = await _httpClient.GetAsync($"{Endpoints.BaseUrl}{Endpoints.Article}");
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
        using var responseMessage = await _httpClient.GetAsync($"{Endpoints.BaseUrl}{Endpoints.Article}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var article = await responseMessage.Content.ReadFromJsonAsync<Article>();
            return article;
        }

        return new Article {IsResponseSuccess = false};
    }

    public Task<bool> Insert(Article entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}