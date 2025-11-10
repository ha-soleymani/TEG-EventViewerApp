

using Domain.Models;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Infrastructure.Extentions;

public static class DataProviderExtentions
{
    public static async Task<string> GetFallbackDataAsync(string contentRootPath, ILogger logger)
    {
        // Better Alternatives for Production-Grade Fallbacks
        // could include caching strategies or resilient data storage solutions.
        // For simplicity, we use a static file here.
        var fallbackPath = Path.Combine(contentRootPath, "Fallback", "events-fallback.json");
        if (!File.Exists(fallbackPath))
        {
            return string.Empty;
        }

        return await File.ReadAllTextAsync(fallbackPath);
    }
}
