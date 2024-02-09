using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using VeggieApp.DataSource.Data;
using VeggieApp.DataSource.Service.AuthenticationService;
using VeggieApp.DataSource.Service.OrderService;
using VeggieApp.DataSource.Service.ProductService;
using VeggieApp.DataSource.Utility;

namespace VeggieApp.Hybrid
{
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
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddSingleton<ProductStore>();
            /*home*/
            /*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://192.168.1.7:5000/") });*/
            /*office*/
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://192.168.1.2:5000/") });
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddBlazorWebViewDeveloperTools();

    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
