using System.Text.RegularExpressions;

namespace Pcm.WebUi.Controller;

public static class MailValidationController
{
    public static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email,
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }
}