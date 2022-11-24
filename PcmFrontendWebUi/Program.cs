using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;
using PcmFrontendWebUi;
using PcmFrontendWebUi.Models;
using PcmFrontendWebUi.Repositories;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IRepository<Person, int>, PersonRepository>();
builder.Services.AddScoped<IRepository<Article, int>, ArticleRepository>();
builder.Services.AddScoped<IRepository<Order, int>, OrderRepository>();
builder.Services.AddScoped<IRepository<ArticleType, int>, ArticleTypeRepository>();
builder.Services.AddScoped<IRepository<Apprenticeship, int>, ApprenticeshipRepository>();

var app = builder.Build();

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