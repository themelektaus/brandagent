using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace Brandagent;

public static class Utils
{
    public static Application App => Application.Current;
    public static Page MainPage => App.MainPage;
    public static INavigation Navigation => MainPage.Navigation;

    public static void Quit() => App.Quit();

    public static bool IsEmpty(string value) => string.IsNullOrEmpty(value);

    public static async Task WaitAsync(Func<bool> condition = null, TimeSpan? timeout = null)
    {
        var start = DateTime.Now;

        while (condition is null || condition.Invoke())
        {
            if (timeout.HasValue && (DateTime.Now - start) >= timeout.Value)
                break;

            await Task.Delay(10);
        }
    }
}
