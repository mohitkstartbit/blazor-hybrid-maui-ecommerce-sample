using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VeggieApp.DataSource.Data;
using VeggieApp.DataSource.Service.AuthenticationService;
using VeggieApp.DataSource.Service.OrderService;
using VeggieApp.DataSource.Service.ProductService;
using VeggieApp.DataSource.Utility;
using VeggieApp.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<ProductStore>();
/*office*/
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://192.168.1.2:5000") });
/*home*/
/*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://192.168.1.9:5000") });*/
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();


await builder.Build().RunAsync();
