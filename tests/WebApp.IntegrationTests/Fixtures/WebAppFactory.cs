using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using WebApi.Client;

namespace WebApp.IntegrationTests.Fixtures;

class WebAppFactory : WebApplicationFactory<Program>, IAsyncDisposable
{
    private readonly WebApiFactory _webApiFactory;

    public WebAppFactory()
    {
        _webApiFactory = new();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var webApiClientDescriptor = services.First(d => d.ServiceType == typeof(IWebApiClient));

            services.Remove(webApiClientDescriptor);

            services.AddTransient<IWebApiClient, WebApiClient>(_ => new WebApiClient(_webApiFactory.CreateClient()));
        });

        builder.UseEnvironment("Development");
    }

    #region Dispose
    private bool _disposed;
    private bool _disposedAsync;

    protected override void Dispose(bool disposing)
    {
        if (_disposed)
            return;


        if (disposing)
        {
            _webApiFactory.Dispose();
        }

        _disposed = true;

        base.Dispose(disposing);
    }

    public override async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        await base.DisposeAsync();
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_disposed)
            return;

        if (_disposedAsync)
            return;

        await _webApiFactory.DisposeAsync();

        _disposedAsync = true;

        Dispose(true);
    }
    #endregion Dispose
}
