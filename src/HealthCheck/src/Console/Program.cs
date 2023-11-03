using System.Diagnostics;

namespace HealthCheck.Console;

internal class Program
{
    internal static async Task<int> Main(string[] args)
    {
        var client = new HttpClient();
        var healthCheckClient = new HealthCheckHttpClient(client);

        bool isHealthy = await healthCheckClient.TryHealthCheckForHost(args[0]);

        if (isHealthy)
            return 0;
        else
            return 1;
    }
}

public class HealthCheckHttpClient
{
    private readonly HttpClient _httpClient;

    public HealthCheckHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> TryHealthCheckForHost(string host)
    {
        bool isHealthy;
        try
        {
            var uriBuilder = new UriBuilder(host);
            var response = await _httpClient.GetAsync(uriBuilder.Uri);
            response.EnsureSuccessStatusCode();

            isHealthy = true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            isHealthy = false;
        }

        return isHealthy;
    }
}
