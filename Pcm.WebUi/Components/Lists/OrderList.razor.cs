using Microsoft.AspNetCore.Components;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Entities;

namespace Pcm.WebUi.Components.Lists;

public partial class OrderList : ComponentBase
{
    private bool _newOrderPopupIsOpen;
    private IEnumerable<Order> _orders = new List<Order>();
    private string _searchString = "";

    [Inject] public IRepository<IOrder, int> OrderRepository { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _orders = await OrderRepository.GetAll() as List<Order>;
    }

    private bool OrderFilter(Order order)
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;
        var properties = new List<string>();
        properties.Add(order.Number);
        properties.Add(order.Shop);
        properties.Add(order.Date.ToString());
        properties.RemoveAll(x => x == null);
        var matchedProperty = properties.FirstOrDefault(x => x.ToLower().Contains(_searchString.ToLower()));
        return matchedProperty != null;
    }

    private void ToggleNewOrderPopover()
    {
        _newOrderPopupIsOpen = !_newOrderPopupIsOpen;
    }
}