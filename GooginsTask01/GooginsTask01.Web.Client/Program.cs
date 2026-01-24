using GooginsTask01.Shared.Services;
using GooginsTask01.Web.Client.Repositories;
using GooginsTask01.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the GooginsTask01.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

builder.Services.AddSingleton<ITodoRepository, MockRepository>();

await builder.Build().RunAsync();
