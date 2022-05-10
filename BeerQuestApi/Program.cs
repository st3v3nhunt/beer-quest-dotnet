using BeerQuestApi.Handlers;
using BeerQuestApi.Repositories;
using BeerQuestApi.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VenueDbContext>(opt => opt.UseInMemoryDatabase("venues").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Venues API",
                Version = "v1"
            });
        });

WebApplication? app = builder.Build();

await LoadDb.Load(app);

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Venues API v1"));
}

app.UseHttpsRedirection();

app.MapGet("/venues", VenuesHandler.GetVenues);
app.MapGet("/venues/{lat}/{lng}", VenuesHandler.GetVenuesByLocation);

app.Run();
