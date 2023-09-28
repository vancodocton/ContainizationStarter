using WebApp.IntegrationTests.Fixtures;

namespace WebApp.IntegrationTests.Pages;

public class WeatherForecastPageTests : IClassFixture<WebAppFixture>
{
    private readonly WebAppFixture _webAppFixture;

    public WeatherForecastPageTests(WebAppFixture webAppFixture)
    {
        _webAppFixture = webAppFixture;
    }

    [Fact]
    public async Task GetPage_Success()
    {
        var client = _webAppFixture.WebAppFactory.CreateClient();

        var response = await client.GetAsync("/WeatherForecast");

        Assert.True(response.IsSuccessStatusCode);
    }
}
