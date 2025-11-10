using Domain.Models;
using Infrastructure.Config;
using Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;
using System.Text;
using Xunit;

namespace Infrastructure.Tests.Services;
using Domain.Models;
using Infrastructure.Config;
using Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RichardSzalay.MockHttp;
using System.Net;
using System.Text;
using Xunit;

public class EventDataProviderTests
{
    private readonly Mock<HttpMessageHandler> _httpHandlerMock = new();
    private readonly Mock<IWebHostEnvironment> _envMock = new();
    private readonly Mock<ILogger<EventDataProvider>> _loggerMock = new();
    private readonly ExternalApiOptions _options = new() { EventDtaEndpoint = "https://fake-endpoint.com" };

    private EventDataProvider CreateProvider(string fallbackJsonPath = null)
    {
        _envMock.Setup(e => e.ContentRootPath).Returns("test-root");
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When(_options.EventDtaEndpoint)
                .Respond("application/json", SampleJson);

        var client = mockHttp.ToHttpClient();
        return new EventDataProvider(client, _envMock.Object, _loggerMock.Object, Options.Create(_options));
    }

    private static string SampleJson => """
    {
      "events": [
        { "id": 1, "name": "Concert", "startDate": "2022-11-10T12:00:00Z", "venueId": 10 }
      ]
    }
    """;

    [Fact]
    public async Task GetEventsByVenueIdAsync_ReturnsEvents_FromApi()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://fake-endpoint.com")
                .Respond("application/json", """
            {
              "events": [
                {
                  "id": 1,
                  "name": "Concert",
                  "startDate": "2022-11-10T12:00:00Z",
                  "venueId": 10
                }
              ]
            }
            """);

        var client = mockHttp.ToHttpClient();
        var envMock = new Mock<IWebHostEnvironment>();
        envMock.Setup(e => e.ContentRootPath).Returns("test-root");

        var provider = new EventDataProvider(client, envMock.Object, Mock.Of<ILogger<EventDataProvider>>(), Options.Create(new ExternalApiOptions
        {
            EventDtaEndpoint = "https://fake-endpoint.com"
        }));

        var result = await provider.GetEventsByVenueIdAsync(10);

        Assert.Single(result);
        Assert.Equal("Concert", result[0].Name);
    }
}