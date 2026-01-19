using CommunityToolkit.Maui;
using GooginsTask01.Services;
using GooginsTask01.Shared.Services;
using Microsoft.Extensions.Logging;

namespace GooginsTask01
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the GooginsTask01.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddSingleton<ITodoRepository, MockRepository>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
