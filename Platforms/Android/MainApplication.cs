﻿using Android.App;
using Android.Runtime;

using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Brandagent
{
    [Application]
    public class MainApplication(nint handle, JniHandleOwnership ownership)
        : MauiApplication(handle, ownership)
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
