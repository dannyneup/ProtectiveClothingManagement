using System.Data;
using Dapper;
using Dapper.FluentMap;
using MySql.Data.MySqlClient;
using Pcm.Infrastructure.RequestModels;
using Pcm.Infrastructure.ResponseModels;
using PcmBackendApi;
using PcmBackendApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GetConnection>(sp =>
    async () =>
    {
        var connectionString = "Server=localhost;Port=3306;Uid=root;Pwd=example;Database=psadb;sslmode=none;";
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "PcmApi");


app.MapGet("/trainings", async (GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = @"SELECT t.id, name, typeFk as type, yearStarted, COUNT(a.id) AS traineeCount
                FROM training t
                LEFT JOIN apprentice a ON t.id = a.trainingFk
                GROUP BY t.id;";
    return con.Query<List<TrainingResponse>>(sql);
});


app.MapPost("/trainings", async (TrainingRequest request, GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = @"INSERT INTO training (name, typeFk, yearStarted)
                VALUES (@Name, @Type, @YearStarted);
                SELECT id, name, typeFk as type, yearStarted
                FROM training
                WHERE id = LAST_INSERT_ID();";
    var result = await con.QueryFirstOrDefaultAsync<TrainingResponse>(sql, request);

    return result != null
        ? Results.Created($"/trainings/{result.Id}", result)
        : Results.BadRequest();
});


/*
app.MapGet("/persons", async (GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = @"
        select person.id as personId,
               person.firstName as firstName,
               person.lastName as lastName,
               apprenticeship.name as apprenticeship,
               person.emailAddress as emailAdress,
               a.id as articleId,
               T.name as type,
               T.manufacturer as manufaturer,
               T.style as style, 
               aC.name as category,
               a.size as size,
               o.number as orderNumber,
               o.date as orderDate,
               a.status as status,
               a.statusChanged as statusChanged
        from person inner join apprenticeship on person.apprenticeship = apprenticeship.id
            left join personOwnsArticle pOA on person.id = pOA.person
            inner join article a on pOA.article = a.id
            inner join articleType T on a.articleType = T.id
            inner join articleCategory aC on T.category = aC.id
            inner join `order` o on a.`order` = o.id;";

    var lookup = new Dictionary<int, Person>();

    return con.Query<Person, Article, Person>(sql, (p, a) =>
    {
        Person person;
        if (!lookup.TryGetValue(p.Id, out person)) lookup.Add(p.Id, person = p);

        person.OwnedArticles ??= new List<Article>();

        person.OwnedArticles.Add(a);
        return person;
    }, splitOn: "articleId").Distinct();
});

app.MapGet("/persons/{id}", async (GetConnection connectionGetter, int id) =>
{
    using var con = await connectionGetter();
    var sql = $@"
        select person.id as personId,
               person.firstName as firstName,
               person.lastName as lastName,
               apprenticeship.name as apprenticeship,
               person.emailAddress as emailAdress,
               a.id as articleId,
               T.name as type,
               T.manufacturer as manufaturer,
               T.style as style, 
               aC.name as category,
               a.size as size,
               o.number as orderNumber,
               o.date as orderDate,
               a.status as status,
               a.statusChanged as statusChanged
        from person inner join apprenticeship on person.apprenticeship = apprenticeship.id
            left join personOwnsArticle pOA on person.id = pOA.person
            inner join article a on pOA.article = a.id
            inner join articleType T on a.articleType = T.id
            inner join articleCategory aC on T.category = aC.id
            inner join `order` o on a.`order` = o.id
        where person.id = {id};";

    var lookup = new Dictionary<int, Person>();

    return con.Query<Person, Article, Person>(sql, (p, a) =>
    {
        Person person;
        if (!lookup.TryGetValue(p.Id, out person)) lookup.Add(p.Id, person = p);

        person.OwnedArticles ??= new List<Article>();

        person.OwnedArticles.Add(a);
        return person;
    }, splitOn: "articleId").Distinct();
});

app.MapGet("/articles", async (GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();
    var sql = @"
        select a.id as articleId,
               T.name as articleType,
               T.manufacturer as manufacturer,
               T.style as style,
               aC.name as category,
               a.size as size,
               o.number as orderNumber,
               o.date as orderDate,
               a.status as status,
               a.statusChanged as statusChanged
        from article a join articleType T on T.id = a.articleType
            inner join articleCategory aC on T.category = aC.id
            inner join `order` o on a.`order` = o.id";
    return con.Query<Article>(sql);
});

app.MapGet("/articles/{id}", async (GetConnection connectionGetter, int id) =>
{
    using var con = await connectionGetter();
    var sql = $@"
        select person.id, 
               person.firstName, 
               person.lastName, 
               a.name as apprenticeship,
               person.emailAddress
        from person join apprenticeship a on person.apprenticeship = a.id
        where person.id = {id}";
    return con.Query<Person>(sql);
});

app.MapGet("/apprenticeships", async (GetConnection connectionGetter) =>
{
    using var con = await connectionGetter();

    var lookup = new Dictionary<int, Apprenticeship>();
    var sql =
        @"
        select a.id as apprenticeshipId, 
            a.name as apprenticeshipName,
            aC.id as articleCategoryId, 
            aC.name as articleCategoryName,
            dAL.count as count
        from apprenticeship a
            inner join defaultApprenticeshipLoadout dAL on a.id = dAL.apprenticeship
            inner join articleCategory aC on dAL.articleCategory = aC.id";

    return con.Query<Apprenticeship, ArticleCategory, int, Apprenticeship>(sql, (a, aC, count) =>
    {
        Apprenticeship apprenticeship;
        if (!lookup.TryGetValue(a.Id, out apprenticeship)) lookup.Add(a.Id, apprenticeship = a);

        apprenticeship.DefaultLoadout ??= new Dictionary<string, int>();

        apprenticeship.DefaultLoadout.Add(aC.Name, count);
        return apprenticeship;
    }, splitOn: "articleCategoryId, count").Distinct();
});

app.MapGet("/apprenticeships/{id}", async (GetConnection connectionGetter, int id) =>
{
    using var con = await connectionGetter();

    var lookup = new Dictionary<int, Apprenticeship>();

    var sql =
        $@"
        select a.id as apprenticeshipId, 
            a.name as apprenticeshipName,
            aC.id as articleCategoryId, 
            aC.name as articleCategoryName,
            daL.count as 'count'
        from apprenticeship a
            inner join defaultApprenticeshipLoadout dAL on a.id = dAL.apprenticeship
            inner join articleCategory aC on dAL.articleCategory = aC.id
        where a.id = {id}";

    return con.Query<Apprenticeship, ArticleCategory, int, Apprenticeship>(sql, (a, aC, count) =>
    {
        Apprenticeship apprenticeship;
        if (!lookup.TryGetValue(a.Id, out apprenticeship)) lookup.Add(a.Id, apprenticeship = a);

        if (apprenticeship.DefaultLoadout == null) apprenticeship.DefaultLoadout = new Dictionary<string, int>();

        apprenticeship.DefaultLoadout.Add(aC.Name, count);
        return apprenticeship;
    }, splitOn: "articleCategoryId, count").Distinct();
});
*/
app.Run();

namespace PcmBackendApi
{
    public delegate Task<IDbConnection> GetConnection();
}