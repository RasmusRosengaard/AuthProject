using Microsoft.EntityFrameworkCore;
using webapi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Fastsæt databasefil via environment variable eller default path
var dbPath = Environment.GetEnvironmentVariable("DB_PATH") 
             ?? @"C:\Users\rasmu\docker-volumes\webapi-data\auth.db";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}")
);

builder.WebHost.UseUrls("http://+:8080");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); 
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();