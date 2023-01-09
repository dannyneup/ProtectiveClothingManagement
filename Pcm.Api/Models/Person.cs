namespace PcmBackendApi.Models;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Apprenticeship { get; set; }
    public string EmailAdress { get; set; }
    public List<Article> OwnedArticles { get; set; }
}