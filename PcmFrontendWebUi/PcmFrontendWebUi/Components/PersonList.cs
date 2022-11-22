using Microsoft.AspNetCore.Components;

namespace PcmFrontendWebUi.Components;

public partial class PersonList : ComponentBase
{
    [Inject]
    public IHttpController HttpController { get; set; }

    [Parameter] public RenderFragment test { get; set; }
    private List<Person> _persons = new();

    protected override async Task OnInitializedAsync()
    {
        _persons = await HttpController.GetAll();
    }
}