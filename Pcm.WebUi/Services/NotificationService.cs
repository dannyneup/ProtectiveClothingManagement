using MudBlazor;
using Pcm.Application.Models;
using Pcm.WebUi.CustomEventArgs;
using Pcm.WebUi.Models;
using Pcm.WebUi.Resources;

namespace Pcm.WebUi.Services;

public class NotificationService
{
    private readonly ISnackbar _snackbar;
    public NotificationService(ISnackbar snackbar, TraineeModel traineeModel, ItemCategoryModel itemCategoryModel, TrainingModel trainingModel)
    {
        _snackbar = snackbar;
        traineeModel.ModelOperationDone += OnModelOperationDone;
        trainingModel.ModelOperationDone += OnModelOperationDone;
        itemCategoryModel.ModelOperationDone += OnModelOperationDone;
    }

    private void OnModelOperationDone(object? sender, ModelOperationDoneEventArgs e)
    {
        var typeString = GetTypeString(e.Object);
        if (!e.Success)
        {
            _snackbar.Add(typeString, Severity.Error);
            return;
        }
        _snackbar.Add(typeString, Severity.Success);
    }

    private string GetTypeString(object eObject)
    {
        if (ReferenceEquals(eObject, typeof(Training))) return Localization.training;
        if (ReferenceEquals(eObject, typeof(IEnumerable<Training>))) return Localization.trainings;
        if (ReferenceEquals(eObject, typeof(Trainee))) return "Localization.trainee";
        if (ReferenceEquals(eObject, typeof(IEnumerable<Trainee>))) return Localization.trainees;
        return "Localization.entry";
    }

}