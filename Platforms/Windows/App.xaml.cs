using Microsoft.Maui;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Hosting;

using Microsoft.UI;
using Microsoft.UI.Windowing;

using WinRT.Interop;

namespace Brandagent.WinUI
{
    public partial class App : MauiWinUIApplication
    {
        const int WINDOW_WIDTH = 440;
        const int WINDOW_HEIGHT = 720;

        public App()
        {
            WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, _) =>
            {
                var handle = WindowNative.GetWindowHandle(handler.PlatformView);
                var id = Win32Interop.GetWindowIdFromWindow(handle);
                var window = AppWindow.GetFromWindowId(id);
                var area = DisplayArea.GetFromWindowId(id, DisplayAreaFallback.Nearest).WorkArea;
                window.MoveAndResize(
                    new(
                        (area.Width - WINDOW_WIDTH) / 2,
                        (area.Height - WINDOW_HEIGHT) / 2,
                        WINDOW_WIDTH,
                        WINDOW_HEIGHT
                    )
                );
                
            });

            InitializeComponent();
        }

        protected override MauiApp CreateMauiApp()
        {
            var app = MauiProgram.CreateMauiApp();

            return app;
        }
    }
}
