using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DiagnosticSource_PoC.Lib;

public static  class DiagnosticSourceExtensions
{
    public static void AddDiagnosticSource(this IServiceCollection services)
    {
        services.TryAddSingleton<HttpRequestAdapter>();
        services.TryAddSingleton<HttpRequestDiagnosticObserver>();
        services.TryAddSingleton<HttpRequestListener>();

        services.AddHostedService<HttpRequestListenerActivationService>();
    }
}
