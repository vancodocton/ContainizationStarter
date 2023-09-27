using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.Client.UnitTests.Fixtures;

public class WebApiClientFixture : IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private bool _disposedValue;

    public WebApiClientFixture()
    {
        _factory = new WebApiFactory();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _factory.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public WebApiClient GetWebApiClient()
    {
        var httpClient = _factory.CreateClient();

        var webapiClient = new WebApiClient(httpClient);

        return webapiClient;
    }
}
