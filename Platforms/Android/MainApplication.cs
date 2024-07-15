using Android.App;
using Android.Runtime;
using AndroidX.AppCompat.App;

using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Brandagent;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(nint handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
    }

    protected override MauiApp CreateMauiApp()
    {
        var app = MauiProgram.CreateMauiApp();

        return app;
    }
}
