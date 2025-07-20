using CommunityToolkit.Maui;
using Microcharts.Maui;
using Trackify.Services;
using Trackify.ViewModels;
using Trackify.Views;
using System.Net.Http;

namespace Trackify;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
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

        // ✅ THIS IS THE CORRECT URL CONFIGURATION
        string baseApiUrl = DeviceInfo.Platform == DevicePlatform.Android
                                ? "http://192.168.1.22:5138" // REPLACE with your PC's IP and the HTTP port
                                : "http://localhost:5138";

        // Use a more reliable handler
        builder.Services.AddSingleton(new HttpClient(new SocketsHttpHandler()) { BaseAddress = new Uri(baseApiUrl) });

        // Register Services, ViewModels, and Pages
        builder.Services.AddSingleton<TransactionService>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<AddTransactionViewModel>();
        builder.Services.AddSingleton<ChartsViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<AddTransactionPage>();
        builder.Services.AddSingleton<ChartsPage>();

        return builder.Build();
    }
}