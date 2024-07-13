using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

using ZXing.Net.Maui.Controls;

namespace Brandagent
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp
                .CreateBuilder()
                .UseMauiApp<App>()
                .UseBarcodeReader();

            var services = builder.Services;

            services.AddMauiBlazorWebView();

#if DEBUG
            services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
