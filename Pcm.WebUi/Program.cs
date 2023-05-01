using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor;
using MudBlazor.Services;
using Pcm.Application.Interfaces.Repositories;
using Pcm.Infrastructure;
using Pcm.Infrastructure.Repositories;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using Pcm.WebUi.Controller;
using Pcm.WebUi.Refactor.Models;
using Pcm.WebUi.Refactor.ViewModels.Container;
using Pcm.WebUi.Refactor.ViewModels.Forms;
using Pcm.WebUi.Refactor.ViewModels.Tables;

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
builder.Services.AddScoped<StringHandleService>();

builder.Services.AddScoped<TrainingModel>();
builder.Services.AddScoped<PersonsModel>();
builder.Services.AddScoped<LoadoutsModel>();
builder.Services.AddScoped<FormViewModel>();
builder.Services.AddScoped<LoadoutFormViewModel>();
builder.Services.AddScoped<TrainingFormViewModel>();
builder.Services.AddScoped<TraineesFormViewModel>();
builder.Services.AddScoped<LoadOutTableViewModel>();
builder.Services.AddScoped<TraineesTableViewModel>();
builder.Services.AddScoped<TrainingsTableViewModel>();
builder.Services.AddScoped<TrainingMultistepEditorViewModel>();

builder.Services.AddSingleton<IRepository<TrainingResponse, TrainingRequest>, Repository<TrainingResponse, TrainingRequest>>();
builder.Services.AddSingleton<IRepository<LoadOutPartResponse, LoadOutPartRequest>, Repository<LoadOutPartResponse, LoadOutPartRequest>>();
builder.Services.AddSingleton<IRepository<ItemCategoryResponse, ItemCategoryRequest>, Repository<ItemCategoryResponse, ItemCategoryRequest>>();
builder.Services.AddSingleton<IRepository<TraineeResponse, TraineeRequest>, Repository<TraineeResponse, TraineeRequest>>();

var app = builder.Build();
app.UseRequestLocalization(new RequestLocalizationOptions()
    .AddSupportedCultures("en-US", "de-DE")
    .AddSupportedUICultures("en-US", "de-DE"));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();