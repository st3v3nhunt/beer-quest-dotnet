using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace BeerQuestApi.Models;

public class Venue
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    [Key]
    public Uri? Url { get; set; }
    public DateTime? Date { get; set; }
    public string? Excerpt { get; set; }
    public Uri? Thumbnail { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Twitter { get; set; }
    [Name("stars_atmosphere")]
    public float? StarsAtmosphere { get; set; }
    [Name("stars_amenities")]
    public float? StarsAmenities { get; set; }
    [Name("stars_beer")]
    public float? StarsBeer { get; set; }
    [Name("stars_value")]
    public float? StarsValue { get; set; }
}

public class VenueDto : Venue
{
    public VenueDto(Venue venue)
    {
        Name = venue.Name;
        Category = venue.Category;
        Url = venue.Url;
        Date = venue.Date;
        Excerpt = venue.Excerpt;
        Thumbnail = venue.Thumbnail;
        Lat = venue.Lat;
        Lng = venue.Lng;
        Address = venue.Address;
        Phone = venue.Phone;
        Twitter = venue.Twitter;
        StarsAmenities = venue.StarsAmenities;
        StarsAtmosphere = venue.StarsAtmosphere;
        StarsBeer = venue.StarsBeer;
        StarsValue = venue.StarsValue;
    }
    public double Distance { get; set; }
}
