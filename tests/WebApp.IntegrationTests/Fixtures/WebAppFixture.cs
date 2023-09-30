using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApp.IntegrationTests.Fixtures;

public class WebAppFixture
{
    internal WebApplicationFactory<Program> WebAppFactory { get; private set; }

    public WebAppFixture()
    {
        WebAppFactory = new WebAppFactory();
    }
}
