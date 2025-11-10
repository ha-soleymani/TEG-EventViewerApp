using Application.UseCases;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Config;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ExternalApiOptions>(
    builder.Configuration.GetSection("ExternalApi")
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI setup

builder.Services.AddHttpClient<IVenueProvider, VenueProvider>();
builder.Services.AddHttpClient<IEventDataProvider, EventDataProvider>();
builder.Services.AddScoped<GetEventsByVenue>();
builder.Services.AddScoped<GetVenues>();

var fallbackPath = Path.Combine(builder.Environment.ContentRootPath, "Fallback", "events.json");
builder.Services.AddSingleton(sp => fallbackPath);

var app = builder.Build();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();