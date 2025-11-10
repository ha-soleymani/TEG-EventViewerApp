using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Config;
using Infrastructure.Extentions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class VenueProvider : IVenueProvider
    {
        private readonly HttpClient _http;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<VenueProvider> _logger;
        private readonly ExternalApiOptions _options;
        public VenueProvider(HttpClient http,
            IWebHostEnvironment env,
            ILogger<VenueProvider> logger, 
            IOptions<ExternalApiOptions> options)
        {
            _http = http;
            _logger = logger;
            _options = options.Value;
            _env = env;
        }

        public async Task<List<Venue>> GetVenuesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching venues from {Endpoint}", _options.EventDtaEndpoint);

                var res = await _http.GetAsync(_options.EventDtaEndpoint);
                res.EnsureSuccessStatusCode();
                var json = await res.Content.ReadAsStringAsync();

                _logger.LogInformation("Successfully fetched venues data");

                return ParseVenues(json);
            }
            catch (Exception ex)
            {
                var fallbackJson = await DataProviderExtentions.GetFallbackDataAsync(_env.ContentRootPath, _logger);

                if (string.IsNullOrEmpty(fallbackJson))
                {
                    _logger.LogError(ex, "Failed to fetch venues and no fallback data available");
                    return [];
                }
                return ParseVenues(fallbackJson);
            }
        }

        private List<Venue> ParseVenues(string json)
        {
            var doc = JsonDocument.Parse(json);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var venues = doc.RootElement.GetProperty("venues").Deserialize<List<Venue>>(options) ?? [];
            return venues.ToList();
        }
    }
}
