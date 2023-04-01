using System.Text.RegularExpressions;

namespace Pcm.WebUi.Controller;

public static class StringHandleController
{
    public static string[] CreateWordArray(string s)
    {
        s = RemoveSpecialCharacters(s);
        s = RemoveMultipleWhitespaces(s);
        var words = s.ToLower().Split(" ");
        return words.Where(w => !string.IsNullOrEmpty(w)).ToArray();
    }

    public static string RemoveMultipleWhitespaces(string s)
    {
        return Regex.Replace(s, @"\s+", " ");
    }

    public static string RemoveSpecialCharacters(string s)
    {
        s = Regex.Replace(s, @"[^A-Za-z0-9 -]", " ");
        return s;
    }
}