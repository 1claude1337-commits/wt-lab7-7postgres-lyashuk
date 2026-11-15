using Microsoft.EntityFrameworkCore;
using Npgsql;
using wt_lab7_7postgres_efcore_lyashuk.Data;
using wt_lab7_7postgres_efcore_lyashuk.Models;

var builder = WebApplication.CreateBuilder(args);

// Пункт 19: Получаем строку подключения (из DATABASE_URL на Render или локально из appsettings)
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
string connectionString;

if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var npgsqlBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port > 0 ? uri.Port : 5432,
        Username = userInfo[0],
        Password = userInfo.Length > 1 ? userInfo[1] : "",
        Database = uri.AbsolutePath.TrimStart('/'),
        SslMode = SslMode.Require
    };
    connectionString = npgsqlBuilder.ToString();
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string not found.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Пункт 23 и 17: Автоматическое применение миграций и заполнение начальными данными (Seeding)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    if (!db.Players.Any())
    {
        db.Players.AddRange(
            new Player { Nickname = "s1mple", Role = "AWPer", Rating = 2850, IsActive = true, Team = "NAVI" },
            new Player { Nickname = "ZywOo", Role = "AWPer", Rating = 2840, IsActive = true, Team = "Vitality" },
            new Player { Nickname = "m0NESY", Role = "AWPer", Rating = 2790, IsActive = true, Team = "G2" }
        );
        db.SaveChanges();
    }
}