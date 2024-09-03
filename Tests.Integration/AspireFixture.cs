using System;
using Aspire.Hosting;
using AspireApp1.ApiService;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests.Integration;

//https://github.com/dotnet/eShop/blob/main/tests/Catalog.FunctionalTests/CatalogApiTests.cs
//https://github.com/dotnet/aspire/discussions/878
public class AspireFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly IHost _app;

    public AspireFixture()
    {
        var options = new DistributedApplicationOptions { AssemblyName = typeof(AspireFixture).Assembly.FullName, DisableDashboard = true };
        var appBuilder = DistributedApplication.CreateBuilder(options);
        _app = appBuilder.Build();
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
        await _app.StopAsync();
        if (_app is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync().ConfigureAwait(false);
        }
        else
        {
            _app.Dispose();
        }
    }

    public async Task InitializeAsync()
    {
        await _app.StartAsync();
    }
}