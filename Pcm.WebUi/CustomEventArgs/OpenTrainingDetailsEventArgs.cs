using Pcm.Application.Models;

namespace Pcm.WebUi.CustomEventArgs;

public class OpenTrainingDetailsEventArgs
{
    public IEnumerable<Trainee> Trainees = default!;
    public IEnumerable<LoadOutPart> LoadOut = default!;
}