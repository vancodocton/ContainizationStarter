using Microsoft.AspNetCore.Mvc.RazorPages;

using WebApi.Client;

namespace WebApp.Pages;

public class WeatherForecastModel : PageModel
{
    private readonly IWebApiClient _webApiClient;

    public WeatherForecastModel(IWebApiClient webApiClient)
    {
        _webApiClient = webApiClient;
    }

    public ICollection<WeatherForecast> WeatherForecasts { get; private set; } = null!;

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        WeatherForecasts = await _webApiClient.GetWeatherForecastAsync(cancellationToken);
    }
}
