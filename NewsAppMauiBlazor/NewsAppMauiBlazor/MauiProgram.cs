using Blazored.Modal;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.AspNetCore.Components.WebView.Maui;
using NewsAppMauiBlazor.Data;
using NewsAppMauiBlazor.Services;

namespace NewsAppMauiBlazor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        builder.Services.AddTransient(x => SecureStorage.Default);
		builder.Services.AddTransient(x => Browser.Default);
		builder.Services.AddTransient(x => Preferences.Default);

		var provider = builder.Services.BuildServiceProvider();
		builder.Services.AddSingleton(x => new LocalStorageService(provider.GetRequiredService<ISecureStorage>()));
		builder.Services.AddTransient<NewsService>();
		builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddBlazoredModal();

		return builder.Build();
	}
}
