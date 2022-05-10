using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using BeerQuestApi.Models;
using BeerQuestApi.Repositories;

namespace BeerQuestApi.Utils;

public class LoadDb
{
    public static async Task Load(WebApplication app)
    {
        using IServiceScope? scope = app.Services.CreateScope();
        VenueDbContext? db = scope.ServiceProvider.GetRequiredService<VenueDbContext>();

        CsvConfiguration config = new(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(CultureInfo.InvariantCulture),
        };

        using StreamReader reader = new(@"./Data/leedsbeerquest.csv");
        using CsvReader csv = new(reader, config);
        IEnumerable<Venue>? venues = csv.GetRecords<Venue>();
        await db.Venues.AddRangeAsync(venues);
        _ = await db.SaveChangesAsync();
    }
}
