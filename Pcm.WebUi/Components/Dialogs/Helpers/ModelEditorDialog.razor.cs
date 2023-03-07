using System.Security.AccessControl;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Pcm.WebUi.Components.Dialogs.Helpers
{
    public partial class ModelEditorDialog : ComponentBase
    {
        [Parameter] public RenderFragment Component { get; set; }

        [Parameter] public Func<Task> SaveModel { get; set; }
        [Parameter] public bool ValidateOnInitialized { get; set; } = false;

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private MudForm _form;
        private bool _isValid;

        protected override void OnAfterRender(bool firstRender)
        {
            if (ValidateOnInitialized)
            {
                _form.Validate();
            }
        }

        private void OnSave()
        {
            SaveModel();
            MudDialog.Close();
        }

        private void OnCancel()
        {
            MudDialog.Cancel();
        }
    }
}