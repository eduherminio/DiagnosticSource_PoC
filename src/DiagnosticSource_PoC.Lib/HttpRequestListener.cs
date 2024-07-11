using System.Diagnostics;

namespace DiagnosticSource_PoC.Lib;

public sealed class HttpRequestListener : IDisposable
{
    private readonly IDisposable _listener;

    public HttpRequestListener(HttpRequestDiagnosticObserver httpRequestDiagnosticObserver)
    {
        _listener = DiagnosticListener.AllListeners.Subscribe(httpRequestDiagnosticObserver);
    }

    public void Dispose()
    {
        _listener.Dispose();
    }
}
