using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using Pcm.Application.Interfaces;
using Pcm.Application.Interfaces.ResponseModels;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure.Repositories;

public class ItemCategoryRepository : IRepository<IItemCategoryResponseModel, IItemCategoryRequestModel>
{
    private readonly string _ItemCategoryRequestUri = $"{Endpoints.BaseUrl}{Endpoints.ItemCategory}";
    private readonly HttpClient _httpClient;
    
    public ItemCategoryRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<IItemCategoryResponseModel>> GetAll()
    {
        try
        {
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return await _httpClient.GetFromJsonAsync<List<ItemCategoryResponseModel>>(_ItemCategoryRequestUri, options);
        }
        catch (Exception e) when (e is HttpRequestException or JsonException)
        {
            Debug.WriteLine(e);
            return new List<ItemCategoryResponseModel>();
        }
    }

    public async Task<IItemCategoryResponseModel> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IItemCategoryResponseModel> Insert(IItemCategoryRequestModel requestModel)
    {
        throw new NotImplementedException();
    }

    public Task<IItemCategoryResponseModel> Update(IItemCategoryResponseModel responseModel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}