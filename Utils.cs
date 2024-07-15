using Microsoft.Maui.Controls;

namespace Brandagent;

public static class Utils
{
    public static INavigation Navigation => App.Current.MainPage.Navigation;

    public static bool IsEmpty(string value) => string.IsNullOrEmpty(value);
}
