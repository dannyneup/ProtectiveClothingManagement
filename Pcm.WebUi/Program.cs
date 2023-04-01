using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor;
using MudBlazor.Services;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure;
using Pcm.Infrastructure.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEndpointService, EndpointService>();

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
});
builder.Services.AddHttpClient();
builder.Services.AddLocalization();

//Dependency injection
builder.Services.AddScoped<IRepository<PersonResponse, PersonRequest>, Repository<PersonResponse, PersonRequest>>();
builder.Services
    .AddScoped<IRepository<ItemCategoryResponse, ItemCategoryRequest>,
        Repository<ItemCategoryResponse, ItemCategoryRequest>>();
builder.Services
    .AddScoped<ITrainingRepository<TrainingResponse, TrainingRequest>, TrainingRepository>();


var app = builder.Build();
app.UseRequestLocalization(new RequestLocalizationOptions()
    .AddSupportedCultures("en-US", "de-DE")
    .AddSupportedUICultures("en-US", "de-DE"));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();