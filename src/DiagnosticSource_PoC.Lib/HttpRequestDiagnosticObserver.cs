using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DiagnosticSource_PoC.Lib;

public sealed class HttpRequestDiagnosticObserver : IObserver<DiagnosticListener>, IDisposable
{
    private const string ListenerName = "Microsoft.AspNetCore";

    private readonly HttpRequestAdapter _httpRequestAdapter;
    private readonly ILogger _logger;
    private readonly List<IDisposable> _observers;

    public HttpRequestDiagnosticObserver(HttpRequestAdapter httpRequestAdapter, ILogger<HttpRequestDiagnosticObserver> logger)
    {
        _httpRequestAdapter = httpRequestAdapter;
        _logger = logger;
        _observers = [];
    }

    public void OnNext(DiagnosticListener value)
    {
         _logger.LogInformation("[Listener] {Listener}", value.Name);
        if (value.Name == ListenerName)
        {
            _logger.LogInformation("[Listener] Subscribing to Listener {ListenerName}", ListenerName);
            _observers.Add(value.SubscribeWithAdapter(_httpRequestAdapter));
        }
    }

    public void OnCompleted()
    {
        // Method intentionally left empty.
    }

    public void OnError(Exception error)
    {
        // Method intentionally left empty.
    }

    public void Dispose()
    {
        foreach (var observer in _observers)
        {
            observer.Dispose();
        }
    }
}
