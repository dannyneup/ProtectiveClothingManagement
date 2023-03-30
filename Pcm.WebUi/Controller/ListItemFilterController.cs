namespace Pcm.WebUi.Controller;

public static class ListItemFilterController<T>
{
    public static bool CheckIfStringMatchesProperties(T item, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        var searchWords = StringHandleController.CreateWordArray(searchString);
        return CheckIfStringArrayMatchesProperties(item, searchWords);
    }

    public static IEnumerable<T> FilterByWords(IEnumerable<T> items, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString)) return items;

        var searchWords = StringHandleController.CreateWordArray(searchString);
        var filteredItems = new List<T>();
        foreach (var item in items)
        {
            var itemMatches = CheckIfStringArrayMatchesProperties(item, searchWords);
            if (itemMatches) filteredItems.Add(item);
        }

        return filteredItems;
    }

    private static bool CheckIfStringArrayMatchesProperties(T item, string[] searchWords)
    {
        foreach (var searchWord in searchWords)
        {
            var properties = item.GetType().GetProperties();
            var foundInProperties = properties.Any(p =>
                (p.GetValue(item) ?? "")
                .ToString()
                .Contains(searchWord, StringComparison.OrdinalIgnoreCase));
            if (foundInProperties) return true;
        }

        return false;
    }
}