using AngleSharp;

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
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var document = await BrowsingContext.New()
            .OpenAsync(res => res.Content(content));

        var weatherForecastTable = document.QuerySelector("table");
        Assert.NotNull(weatherForecastTable);

        var thCols = weatherForecastTable.QuerySelectorAll("thead tr th");
        Assert.NotNull(thCols);

        var columnHeadersExpect = new string[] { "Date", "Temp. (C)", "Temp. (F)", "Summary" };
        var columnHeadersActual = thCols.Select(th => th.TextContent).ToArray();
        Assert.Equal(columnHeadersExpect, columnHeadersActual);

        var rows = weatherForecastTable.QuerySelectorAll("tbody tr");
        Assert.Equal(5, rows.Length);

        foreach (var row in rows)
        {
            Assert.Equal(4, row.ChildElementCount);
        }
    }
}
