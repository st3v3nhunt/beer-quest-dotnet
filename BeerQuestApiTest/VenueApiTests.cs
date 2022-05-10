using BeerQuestApi.Models;
using BeerQuestApi.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Collections.Generic;
using System.Net.Http.Json;
using Xunit;

namespace BeerQuestApiTest;

public class VenueApiTests
{
    [Fact]
    public async void GetVenues()
    {
        await using ApiApplication? application = new();

        System.Net.Http.HttpClient? client = application.CreateClient();
        List<Venue>? venues = await client.GetFromJsonAsync<List<Venue>>("/venues");

        Assert.Equal(242, venues.Count);
    }

    [Fact]
    public async void GetVenuesFiltered()
    {
        await using ApiApplication? application = new();

        System.Net.Http.HttpClient? client = application.CreateClient();
        List<Venue>? venues = await client.GetFromJsonAsync<List<Venue>>("/venues?amenities=4&atmosphere=4&beer=4&value=4");

        Assert.Equal(3, venues.Count);
    }

    [Fact]
    public async void GetVenuesByLocation()
    {
        await using ApiApplication? application = new();

        System.Net.Http.HttpClient? client = application.CreateClient();
        List<Venue>? venues = await client.GetFromJsonAsync<List<Venue>>("/venues/54/-1.5");

        Assert.Equal(242, venues.Count);
    }

    [Fact]
    public async void GetVenuesByLocationFiltered()
    {
        await using ApiApplication? application = new();

        System.Net.Http.HttpClient? client = application.CreateClient();
        List<Venue>? venues = await client.GetFromJsonAsync<List<Venue>>("/venues/54/-15?amenities=4&atmosphere=4&beer=4&value=4");

        Assert.Equal(3, venues.Count);
    }
}

internal class ApiApplication : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        InMemoryDatabaseRoot? root = new();

        _ = builder.ConfigureServices(services =>
        {
            _ = services.RemoveAll(typeof(DbContextOptions<VenueDbContext>));

            _ = services.AddDbContext<VenueDbContext>(options => options.UseInMemoryDatabase("Testing", root));
        });

        return base.CreateHost(builder);
    }
}
