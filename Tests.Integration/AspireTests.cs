using System.Net.Http;
using System.Text.Json;
using Asp.Versioning;
using Asp.Versioning.Http;
using AspireApp1.ApiService;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests.Integration;

public sealed class AspireTests : IClassFixture<AspireFixture>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web);

    public AspireTests(AspireFixture fixture)
    {
        var handler = new ApiVersionHandler(new QueryStringApiVersionWriter(), new ApiVersion(1.0));

        _webApplicationFactory = fixture;
        _httpClient = _webApplicationFactory.CreateDefaultClient(handler);
    }

    [Fact]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/weatherforecast");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}