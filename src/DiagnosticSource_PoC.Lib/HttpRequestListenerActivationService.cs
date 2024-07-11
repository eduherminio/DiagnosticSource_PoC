using Microsoft.Extensions.Hosting;

namespace DiagnosticSource_PoC.Lib;

/// <summary>
/// Activates <see cref="HttpRequestListener"/> at application startup.
/// This service provides a manual activation workaround for types registered as a singleton and never resolved.
/// </summary>
public sealed class HttpRequestListenerActivationService : IHostedService
{
    public HttpRequestListenerActivationService(HttpRequestListener httpRequestListener) => _ = httpRequestListener;

    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
