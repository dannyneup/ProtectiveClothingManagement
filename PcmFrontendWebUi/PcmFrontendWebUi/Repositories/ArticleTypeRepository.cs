using PcmFrontendWebUi.Models;

namespace PcmFrontendWebUi.Repositories;

public class ArticleTypeRepository : IRepository<ArticleType, int>
{
    private readonly HttpClient _httpClient;


    public ArticleTypeRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<IEnumerable<ArticleType>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<ArticleType> Get(int id)
    {
        using var responseMessage = await _httpClient.GetAsync($"{Endpoints.BaseUrl}{Endpoints.ArticleType}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var articleType = await responseMessage.Content.ReadFromJsonAsync<ArticleType>();
            return articleType;
        }

        return new ArticleType {IsResponseSuccess = false};
    }

    public Task<bool> Insert(ArticleType entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}