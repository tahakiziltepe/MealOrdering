using Blazored.Modal;
using MealOrdering;
using MealOrdering.Utils;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MealOrdering.Services.Extensions;
using MealOrdering.Services.Infrastructure;
using MealOrdering.Services.Services;
using MealOrdering.Server.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { 
	BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
	});

builder.Services.AddScoped<ModalManager>();
builder.Services.AddBlazoredModal();

builder.Services.ConfigureMapping();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<MealOrderingDbContext>(config =>
{
	config.UseNpgsql("Host=localhost;Port=5432;Database=mealordering;Username=postgres;Password=example;");
	config.EnableSensitiveDataLogging();
});

await builder.Build().RunAsync();
