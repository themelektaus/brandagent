using Microsoft.Maui.Controls;

namespace Brandagent;

public static class Utils
{
    public static Application App => Application.Current;
    public static Page MainPage => App.MainPage;
    public static INavigation Navigation => MainPage.Navigation;
    public static void Quit() => App.Quit();

    public static bool IsEmpty(string value) => string.IsNullOrEmpty(value);
}
