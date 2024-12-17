using Microsoft.EntityFrameworkCore;
using TechTestBackend;
using TechTestBackend.Domain;
using TechTestBackend.Domain.Interfaces;
using TechTestBackend.Domain.Middleware;
using TechTestBackend.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddScoped<ISpotifyHelper, SpotifyHelper>();
builder.Services.AddScoped<ISongsService, SongsService>();
builder.Services.AddScoped<ISongsRepository, SongsRepository>();
builder.Services.AddHttpClient();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<SongStorageContext>(options => options.UseInMemoryDatabase("SongStorage"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseMiddleware<ErrorHandlerMiddleware>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();