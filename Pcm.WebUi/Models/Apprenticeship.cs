using Pcm.Core;
using Pcm.Core.Entities;

namespace Pcm.WebUi.Models;

public class Apprenticeship : ResponseBase, IApprenticeship
{
    public int Id { get; set; }
    public string Name { get; set; }
}