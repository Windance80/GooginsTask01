// using GooginsTask01.Shared.Data.Repositories;
using GooginsTask01.Shared.Services;
using GooginsTask01.Web.Client.Repositories;
using GooginsTask01.Web.Components;
using GooginsTask01.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Add device-specific services used by the GooginsTask01.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

builder.Services.AddSingleton<ITodoRepository, MockRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(GooginsTask01.Shared._Imports).Assembly,
        typeof(GooginsTask01.Web.Client._Imports).Assembly);

app.Run();
