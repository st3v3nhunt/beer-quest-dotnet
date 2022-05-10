using BeerQuestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerQuestApi.Repositories;

public class VenueDbContext : DbContext
{
    public VenueDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Venue> Venues { get; set; } = null!;
}
