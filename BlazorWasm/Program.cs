using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasm;
using GooginsTask01.Shared.Services;
using BlazorWasm.Services;
using GooginsTask01.Shared.Data.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<ITodoRepository, MockRepository>();

builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
