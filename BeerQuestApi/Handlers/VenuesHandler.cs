using BeerQuestApi.Models;
using BeerQuestApi.Repositories;
using Geolocation;
using Microsoft.EntityFrameworkCore;

namespace BeerQuestApi.Handlers;

public class VenuesHandler
{
    private static async Task<IEnumerable<Venue>> GetFilteredVenues(VenueDbContext venueRepository, string? amenities, string? atmosphere, string? beer, string? value)
    {
        _ = float.TryParse(amenities, out float amenityStars);
        _ = float.TryParse(atmosphere, out float atmosphereStars);
        _ = float.TryParse(beer, out float beerStars);
        _ = float.TryParse(value, out float valueStars);

        return await venueRepository.Venues
            .Where(v => v.StarsAmenities >= amenityStars)
            .Where(v => v.StarsAtmosphere >= atmosphereStars)
            .Where(v => v.StarsBeer >= beerStars)
            .Where(v => v.StarsValue >= valueStars)
            .ToListAsync();
    }

    public static async Task<IEnumerable<Venue>> GetVenues(VenueDbContext db, string? amenities, string? atmosphere, string? beer, string? value)
    {
        return await GetFilteredVenues(db, amenities, atmosphere, beer, value);
    }

    public static async Task<IEnumerable<VenueDto>> GetVenuesByLocation(VenueDbContext db, double lat, double lng, string? amenities, string? atmosphere, string? beer, string? value)
    {
        Coordinate point = new(lat, lng);
        IEnumerable<Venue>? venues = await GetFilteredVenues(db, amenities, atmosphere, beer, value);

        return venues.Select(v =>
        {
            return new VenueDto(v)
            {
                Distance = GeoCalculator.GetDistance(point, new Coordinate(v.Lat, v.Lng))
            };
        })
        .OrderBy(v => v.Distance);
    }
}
