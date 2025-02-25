namespace Devon4Net.Infrastructure.Logger.Enrichers;

using Microsoft.Extensions.Configuration;
using Serilog;
using Devon4Net.Infrastructure.Logger.Options;

internal static class LogEnrichersExtensions
{
    public static LoggerConfiguration ConfigureLogEnrichers(this LoggerConfiguration loggerConfiguration, IConfiguration configuration)
    {
        var loggingOptions = configuration.GetSection(LoggingOptions.SectionName).Get<LoggingOptions>();

        return loggerConfiguration
            .Enrich.WithProperty("ApplicationName", loggingOptions.ApplicationName)
            .Enrich.With(new CorrelationIdEnricher());
    }
}
