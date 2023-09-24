using WebApi.Client.UnitTests.Fixtures;

namespace WebApi.Client.UnitTests;

public class TestWeatherForecast : IClassFixture<WebApiClientFixture>
{
    private readonly WebApiClientFixture _webApiClientFixture;

    public TestWeatherForecast(WebApiClientFixture webApiClientFixture)
    {
        _webApiClientFixture = webApiClientFixture;
    }

    [Fact]
    public async Task TestGetWeatherForecastAsync()
    {
        var client = _webApiClientFixture.GetWebApiClient();

        var results = await client.GetWeatherForecastAsync();

        Assert.NotNull(results);
        Assert.NotEmpty(results);
    }
}
