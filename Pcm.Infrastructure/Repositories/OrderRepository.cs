using System.Net.Http.Json;
using System.Text.Json;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.Infrastructure.Repositories;

public class OrderRepository : IRepository<IOrder, int>
{
    private static readonly string _orderEndpoint = $"{Endpoints.BaseUrl}{Endpoints.Order}";
    private readonly HttpClient _httpClient;

    public OrderRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<IOrder>> GetAll()
    {
        using var responseMessage = await _httpClient.GetAsync(_orderEndpoint);
        if (responseMessage.IsSuccessStatusCode)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);
            var root = doc.RootElement.GetProperty("orders");
            var orders = root.Deserialize<List<Order>>(new JsonSerializerOptions
                {PropertyNameCaseInsensitive = true});
            return orders;
        }

        return new List<IOrder>();
    }

    public async Task<IOrder> Get(int id)
    {
        using var responseMessage = await _httpClient.GetAsync($"{Endpoints.BaseUrl}{Endpoints.Order}/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var order = await responseMessage.Content.ReadFromJsonAsync<Order>();
            return order;
        }

        return new Order {IsResponseSuccess = false};
    }

    public Task<bool> Insert(IOrder entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}