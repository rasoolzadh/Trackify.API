using CommunityToolkit.Maui;
using Microcharts.Maui;
using Trackify.Services;
using Trackify.ViewModels;
using Trackify.Views;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

using System.Globalization;

namespace Trackify;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMicrocharts()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Configure HttpClient
        builder.Services.AddSingleton(sp =>
        {
            var handler = new HttpClientHandler();
            var baseUrl = GetBaseApiUrl();

#if DEBUG
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                // Bypass SSL certificate validation in debug mode
                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true;
            }
#endif

            return new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };
        });

        // Register Services
        builder.Services.AddSingleton<TransactionService>();

        // Register ViewModels and Pages
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<AddTransactionViewModel>();
        builder.Services.AddSingleton<ChartsViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<AddTransactionPage>();
        builder.Services.AddSingleton<ChartsPage>();

        return builder.Build();
    }

    private static string GetBaseApiUrl()
    {
        // For Android emulator
        if (DeviceInfo.Platform == DevicePlatform.Android && Debugger.IsAttached)
        {
            return "http://10.0.2.2:5138";
        }

        // For physical Android device
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            return "http://192.168.1.39:5138";
        }

        // For Windows/Mac
        return "http://localhost:5138";
    }
}