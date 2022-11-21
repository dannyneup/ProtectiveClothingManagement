using PcmFrontendWebUi.Pages;

namespace PcmFrontendWebUi;

public class NewPerson
{
    public int Id { get; set; }
    public string FirtstName { get; set; }
    public string LastName { get; set; }
    public Apprenticeship Apprenticeship { get; set; }
}