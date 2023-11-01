using System.Diagnostics;

namespace HealthCheck.Console;

internal class Program
{
    internal static async Task<int> Main(string[] args)
    {
        var client = new HttpClient();

        bool isHealthy = false;
        try
        {
            var uriBuilder = new UriBuilder(args[0]);
            var response = await client.GetAsync(uriBuilder.Uri);
            if (response.IsSuccessStatusCode)
                isHealthy = true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            isHealthy = false;
        }

        if (isHealthy)
            return 0;
        else
            return 1;
    }
}
