using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Identity;
using Radzen;
using VeggieApp.DataSource.Data;
using VeggieApp.DataSource.Service.AdminService;
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
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000/") });
/*home*/
/*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://192.168.1.9:5000") });*/
// Pick the correct one based on where your actual provider class is
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddRadzenComponents();
builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>();

await builder.Build().RunAsync();
