namespace PcmFrontendWebUi.Models;

public class Person : ResponseBase
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Apprenticeship Apprenticeship { get; set; }
    public string emailAdress { get; set; }
}