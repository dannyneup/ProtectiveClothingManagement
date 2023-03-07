using Pcm.Infrastructure.ResponseModels;

namespace Pcm.Infrastructure.RequestModels;

public class PersonInfoRequestModel : IPersonInfoRequestModel
{
    public bool IsResponseSuccess { get; set; }
    public int PersonnelNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string TrainingName { get; set; }
    public string TrainingType { get; set; }
    public string YearStarted { get; set; }
}