using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;
using Pcm.Application.Interfaces;
using Pcm.Core.Entities;
using Pcm.Infrastructure.Repositories;
using Pcm.WebUi.Models;


var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddHttpClient();
builder.Services.AddLocalization();

//Dependency injection
builder.Services.AddScoped<IRepository<IPerson, int>, PersonRepository>();
//builder.Services.AddScoped<IRepository<Article, int>, ArticleRepository>();
//builder.Services.AddScoped<IRepository<ArticleCategory, int>, ArticleCategoryRepository>();
//builder.Services.AddScoped<IRepository<ArticleType, int>, ArticleTypeRepository>();
//builder.Services.AddScoped<IRepository<Order, int>, OrderRepository>();
//builder.Services.AddScoped<IRepository<Apprenticeship, int>, ApprenticeshipRepository>();
//builder.Services.AddScoped<INewArticleViewModel, NewArticleViewModel>();

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