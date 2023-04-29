namespace Pcm.WebUi.Controller;

public class ListItemFilterService<T>
{
    private StringHandleService _stringHandleService;
    public ListItemFilterService(StringHandleService stringHandleService)
    {
        _stringHandleService = stringHandleService;
    }
    
    public bool CheckIfStringMatchesProperties(T item, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        var searchWords = _stringHandleService.CreateWordArray(searchString);
        return CheckIfStringArrayMatchesProperties(item, searchWords);
    }

    public IEnumerable<T> FilterByWords(IEnumerable<T> items, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString)) return items;

        var searchWords = _stringHandleService.CreateWordArray(searchString);
        var filteredItems = new List<T>();
        foreach (var item in items)
        {
            var itemMatches = CheckIfStringArrayMatchesProperties(item, searchWords);
            if (itemMatches) filteredItems.Add(item);
        }

        return filteredItems;
    }

    private bool CheckIfStringArrayMatchesProperties(T item, IEnumerable<string> searchWords)
    {
        foreach (var searchWord in searchWords)
        {
            var properties = item!.GetType().GetProperties();
            var foundInProperties = properties.Any(p =>
                (p.GetValue(item) ?? "")
                .ToString()!
                .Contains(searchWord, StringComparison.OrdinalIgnoreCase));
            if (foundInProperties) return true;
        }

        return false;
    }
}