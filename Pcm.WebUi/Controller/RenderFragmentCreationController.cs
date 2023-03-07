using System.Collections.Immutable;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Pcm.WebUi.Controller;

public static class RenderFragmentCreationController
{
    public static RenderFragment CreateRenderFragmentFromComponent<T>(IDictionary<string, object> parameters = null) => builder =>
    {
        builder.OpenComponent(0, typeof(T));
        if (parameters != null)
        {
            foreach (var parameter in parameters)
            {
                builder.AddAttribute(1, parameter.Key, parameter.Value);
            }
        }
        builder.CloseComponent();
    };

}