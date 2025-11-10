using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Config;
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
    public class EventDataProvider : IEventDataProvider
    {
        private readonly HttpClient _http;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<EventDataProvider> _logger;
        private readonly ExternalApiOptions _options;

        public EventDataProvider(HttpClient http, IWebHostEnvironment env, ILogger<EventDataProvider> logger, IOptions<ExternalApiOptions> options)
        {
            _http = http;
            _env = env;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<List<Event>> GetEventsByVenueIdAsync(int venueId)
        {
            try
            {
                var res = await _http.GetAsync(_options.EventDtaEndpoint);
                res.EnsureSuccessStatusCode();
                var json = await res.Content.ReadAsStringAsync();
                return ParseEvents(json, venueId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Primary source failed. Using fallback data.");
                var fallbackPath = Path.Combine(_env.ContentRootPath, "Fallback", "events-fallback.json");
                if (!File.Exists(fallbackPath))
                {
                    _logger.LogError("Fallback file missing at {Path}", fallbackPath);
                    return [];
                }

                var fallbackJson = await File.ReadAllTextAsync(fallbackPath);
                return ParseEvents(fallbackJson, venueId);
            }
        }

        private List<Event> ParseEvents(string json, int venueId)
        {
            var doc = JsonDocument.Parse(json);
            
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var events = doc.RootElement.GetProperty("events").Deserialize<List<Event>>(options) ?? [];
            return events.Where(e => e.VenueId == venueId).ToList();
        }
    }
}
