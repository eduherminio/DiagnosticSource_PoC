using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;

namespace DiagnosticSource_PoC.Lib;

public class HttpRequestAdapter
{
    private const string ContextToken = "ContextToken";
    private readonly ILogger _logger;

    public HttpRequestAdapter(ILogger<HttpRequestAdapter> logger)
    {
        _logger = logger;
    }

    [DiagnosticName("Microsoft.AspNetCore.Hosting.HttpRequestIn")]
    public virtual void OnHttpRequestIn()
    {
        // This won't be invoked. This is needed just to add subscription for top level namespace,
        // because the http handler diagnostics listener check for this subscription to be present
        // before emitting request start or end events.
    }

    [DiagnosticName("Microsoft.AspNetCore.Hosting.HttpRequestIn.Start")]
    public void OnHttpRequestInStart(HttpContext httpContext)
    {
        httpContext.Items[ContextToken] = httpContext.Request.GetDisplayUrl();

        _logger.LogInformation("[OnHttpRequestInStart] Start of {Token}", httpContext.Items[ContextToken]);
    }

    [DiagnosticName("Microsoft.AspNetCore.Hosting.HttpRequestIn.Stop")]
    public virtual void OnHttpRequestInStop(HttpContext httpContext)
    {
        if (!httpContext.Items.TryGetValue(ContextToken, out var contextToken))
        {
            return;
        }

        try
        {
            _logger.LogInformation("[OnHttpRequestInStop] End of {Token}", contextToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(OnHttpRequestInStop));
        }
    }
}
