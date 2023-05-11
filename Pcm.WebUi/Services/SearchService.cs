namespace Pcm.WebUi.Services;

public class SearchService
{
    public static List<T> Search<T>(IEnumerable<T> objects, string searchQuery)
    {
        var searchTerms = ExtractSearchTerms(searchQuery);

        var results = objects.Where(obj => Search(obj, searchTerms)).ToList();

        return results;
    }
    
    public static bool Search(object obj, string searchQuery)
    {
        var searchTerms = ExtractSearchTerms(searchQuery);
        return Search(obj, searchTerms);
    }

    private static string[] ExtractSearchTerms(string searchQuery)
    {
        var searchTerms = searchQuery.ToLower().Split(new char[]{' ', ','}, StringSplitOptions.RemoveEmptyEntries);
        return searchTerms;
    }
    
    private static bool Search<T>(T obj, string[] searchTerms)
    {
        var properties = obj.GetType().GetProperties();

        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj) as string;

            if (string.IsNullOrEmpty(value))
                continue;
            
            value = value.ToLower();

            if (searchTerms.All(term => value.Contains(term)))
            {
                return true;
            }
        }
        return false;
    }
}