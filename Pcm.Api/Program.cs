using System.Data;
using Dapper;
using Dapper.FluentMap;
using MySql.Data.MySqlClient;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using PcmBackendApi;
using PcmBackendApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GetConnection>(sp =>
    async () =>
    {
        // TODO: AllowPublicKeyRetrieval=true muss raus und ssl/tls aud required!
        var connectionString = "Server=localhost;Port=3306;Uid=root;Pwd=example;Database=psadb;AllowPublicKeyRetrieval=true;sslmode=none;";
        var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    });

FluentMapper.Initialize(config =>
{
    config.AddMap(new ArticleMap());
    config.AddMap(new ArticleCategoryMap());
    config.AddMap(new ApprenticeshipMap());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "PcmApi");


app.MapGet("/trainings", async (GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = @"SELECT t.id, description AS title, typeFk as type, t.yearStarted AS yearStarted, COUNT(a.id) AS traineeCount
                FROM training t
                LEFT JOIN apprentice a ON t.id = a.trainingFk
                GROUP BY t.id;";
    return con.Query<TrainingResponse>(sql);
});


app.MapPost("/trainings", async (TrainingRequest request, GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = @"INSERT INTO training (description, typeFk, yearStarted)
                VALUES (@Title, @Type, @YearStarted);
                SELECT id, description as title, typeFk as type, yearStarted
                FROM training
                WHERE id = LAST_INSERT_ID();";
    var result = await con.QueryFirstOrDefaultAsync<TrainingResponse>(sql, request);

    return result != null
        ? Results.Created($"/trainings/{result.Id}", result)
        : Results.BadRequest();
});


app.MapGet("/loadouts", async (GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = @"
                SELECT loadout.id as id, c.id as categoryId, c.description as categoryName, count, t.id AS trainingId
                FROM loadout
                JOIN category c on c.id = loadout.categoryFk
                JOIN training t on t.id = loadout.trainingFk;";
    return con.Query<LoadOutPartResponse>(sql);
});


app.MapPut("/loadouts/{id}", async (LoadOutPartRequest request, GetConnection connectionGetter, int id) =>
{
    using var con = await connectionGetter();
    var sql = $@"UPDATE loadout 
                    SET categoryFk = @CategoryId, trainingFk = @TrainingId, count = @Count
                    WHERE id = {id};
                SELECT loadout.id AS id, c.id as categoryId, c.description as categoryName, count, t.id AS trainingId
                FROM loadout
                JOIN category c on c.id = loadout.categoryFk
                JOIN training t on t.id = loadout.trainingFk
                WHERE loadout.id = {id};";
    var result = await con.QueryFirstOrDefaultAsync<LoadOutPartResponse>(sql, request);

    return result != null
        ? Results.Accepted($"/loadouts/{result.Id}", result)
        : Results.BadRequest();
});


app.MapPost("/loadouts", async (LoadOutPartRequest request, GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = @"INSERT INTO loadout (categoryFk, trainingFk, count)
                VALUES (@CategoryId, @TrainingId, @Count);
                SELECT loadout.id AS id, c.id as categoryId, c.description as categoryName, count, t.id AS trainingId
                FROM loadout
                JOIN category c on c.id = loadout.categoryFk
                JOIN training t on t.id = loadout.trainingFk
                WHERE loadout.id = LAST_INSERT_ID();";
    var result = await con.QueryFirstOrDefaultAsync<LoadOutPartResponse>(sql, request);

    return result != null
        ? Results.Created($"/loadouts/{result.Id}", result)
        : Results.BadRequest();
});


app.MapGet("/trainees", async (GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = @"SELECT a.id AS personnelNumber, firstName, lastName, mail AS emailAddress, 
                        a.yearStarted as yearStarted, trainingFk AS trainingId, t.description AS trainingTitle, 
                        tt.type AS trainingType
                FROM apprentice a
                    JOIN training t on t.id = a.trainingFk
                    JOIN training_type tt on tt.type = t.typeFk";
    return con.Query<TraineeResponse>(sql);
});


app.MapPost("/trainees", async (TraineeRequest request, GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = $@"INSERT INTO apprentice (id, firstname, lastname, mail, trainingFk, yearStarted)
                VALUES (@PersonnelNumber, @FirstName, @LastName, @EmailAddress, @TrainingId, @YearStarted);
                SELECT a.id AS personnelNumber, firstName, lastName, mail AS emailAddress, a.yearStarted AS yearStarted, 
                       trainingFk AS trainingId, t.description AS trainingTitle, tt.type AS trainingType
                FROM apprentice a
                    JOIN training t on t.id = a.trainingFk
                    JOIN training_type tt on tt.type = t.typeFk
                WHERE a.id = {request.PersonnelNumber};";
    var result = await con.QueryFirstOrDefaultAsync<TraineeResponse>(sql, request);

    return result != null
      ? Results.Created($"/trainees/{result.PersonnelNumber}", result)
      : Results.BadRequest();
});


app.MapPut("/trainees/{id}", async (TraineeRequest request, GetConnection connectionGetter, int id) =>
{
    using var con = await connectionGetter();
    var sql = $@"UPDATE apprentice 
                    SET firstName = @FirstName, lastName = @LastName, mail = @EmailAddress, trainingFk = @TrainingId
                    WHERE id = {id};
                SELECT a.id AS personnelNumber, firstName, lastName, mail AS emailAddress, a.yearStarted AS yearStarted, 
                       trainingFk AS trainingId, t.description AS trainingTitle, tt.type AS trainingType
                FROM apprentice a
                    JOIN training t on t.id = a.trainingFk
                    JOIN training_type tt on tt.type = t.typeFk
                WHERE a.id = {id};";
    var result = await con.QueryFirstOrDefaultAsync<TraineeResponse>(sql, request);

    return result != null
        ? Results.Accepted($"/trainees/{result.PersonnelNumber}", result)
        : Results.BadRequest();
});


app.MapGet("/items/categories", async (GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = @"
                SELECT id, description as title
                FROM category;";
    return con.Query<ItemCategoryResponse>(sql);
});

app.MapPost("/items/categories", async (ItemCategoryRequest request, GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = @"INSERT INTO category (description)
                VALUES (@Title);
                SELECT id, description as title
                FROM category
                WHERE id = LAST_INSERT_ID();";
    var result = await con.QueryFirstOrDefaultAsync<ItemCategoryResponse>(sql, request);

    return result != null
        ? Results.Created($"/items/categories{result.Id}", result)
        : Results.BadRequest();
});


app.Run();

namespace PcmBackendApi
{
    public delegate Task<IDbConnection> GetConnection();
}