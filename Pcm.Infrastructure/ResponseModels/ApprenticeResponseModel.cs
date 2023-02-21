using Pcm.Core.Entities;

namespace Pcm.Infrastructure.ResponseModels;

public class ApprenticeResponseModel : IApprentice
{
    public int Id { get; init;}
    public string FirstName { get; init;}
    public string LastName { get; init;}
}