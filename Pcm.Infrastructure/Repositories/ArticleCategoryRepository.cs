using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.Infrastructure.Repositories;

public class ArticleCategoryRepository : IRepository<IArticleCategory, int>
{
    private static readonly string _articleCategoryEndpoint = $"{Endpoints.BaseUrl}{Endpoints.ArticleCategory}";
    private readonly HttpClient _httpClient;


    public ArticleCategoryRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<IArticleCategory>> GetAll()
    {
        using var responseMessage = await _httpClient.GetAsync($"{_articleCategoryEndpoint}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);
            var root = doc.RootElement.GetProperty("articleCategories");
            var articleCategory = root.Deserialize<List<ArticleCategory>>(new JsonSerializerOptions
                {PropertyNameCaseInsensitive = true});
            return articleCategory;
        }

        return new List<ArticleCategory>();
    }

    public async Task<IArticleCategory> Get(int id)
    {
        using var responseMessage = await _httpClient.GetAsync($"{_articleCategoryEndpoint}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var articleCategory = await responseMessage.Content.ReadFromJsonAsync<ArticleCategory>();
            return articleCategory;
        }

        return new ArticleCategory {IsResponseSuccess = false};
    }

    public async Task<bool> Insert(IArticleCategory entity)
    {
        var json = new StringContent(JsonSerializer.Serialize(entity),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        using var responseMessage = await _httpClient.PostAsync(_articleCategoryEndpoint, json);
        return responseMessage.IsSuccessStatusCode;
    }


    public async Task<bool> Delete(int id)
    {
        using var responseMessage = await _httpClient.DeleteAsync($"{_articleCategoryEndpoint}/{id}");
        return responseMessage.IsSuccessStatusCode;
    }
}