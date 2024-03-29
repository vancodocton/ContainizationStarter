using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Client;

public static class DependenciesExtensions
{
    public static IServiceCollection AddWebApiClient(this IServiceCollection services, string baseAddress)
    {
        services.AddHttpClient<IWebApiClient, WebApiClient>(client =>
        {
            client.BaseAddress = new Uri(baseAddress);
        });

        return services;
    }
}
