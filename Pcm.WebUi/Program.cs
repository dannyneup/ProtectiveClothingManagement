using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor;
using MudBlazor.Services;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure;
using Pcm.Infrastructure.DTOs;
using Pcm.Infrastructure.DTOs.RequestModels;
using Pcm.Infrastructure.DTOs.ResponseModels;
using Pcm.Infrastructure.Repositories;
using PersonDto = Pcm.Infrastructure.DTOs.PersonDto;

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
builder.Services.AddScoped<IRepository<PersonDto, PersonDto>, Repository<PersonDto, PersonDto>>();
builder.Services
    .AddScoped<IRepository<ItemCategoryDto, ItemCategoryDto>,
        Repository<ItemCategoryDto, ItemCategoryDto>>();
builder.Services
    .AddScoped<ITrainingRepository<TrainingResponse, TrainingRequest, LoadOutPartResponse>, TrainingRepository>();


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