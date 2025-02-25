namespace Devon4Net.Infrastructure.Logger;

using Microsoft.Extensions.Hosting;
using Devon4Net.Infrastructure.Logger.Outputs.File;
using Devon4Net.Infrastructure.Logger.Outputs.GrayLog;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Settings.Configuration;
using Devon4Net.Infrastructure.Logger.Outputs.Console;
using Devon4Net.Infrastructure.Logger.Outputs.SqLiteDb;
using Devon4Net.Infrastructure.Logger.Enrichers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Devon4Net.Infrastructure.Logger.Options;

public static class LoggingConfiguration
{
    public static void SetupLogging(this IHostBuilder builder)
    {
        Action<HostBuilderContext, IServiceProvider, LoggerConfiguration> configureLogger = (context, _, loggerConfiguration) => loggerConfiguration.ConfigureDevon4NetLogging(context.Configuration);

        builder.UseSerilog(configureLogger);
    }

    public static void SetupLogging(this IServiceCollection services, IConfiguration configuration)
    {
        var loggerConfiguration = new LoggerConfiguration();
        ConfigureDevon4NetLogging(loggerConfiguration, configuration);

        Log.Logger = loggerConfiguration.CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        });
    }

    public static LoggerConfiguration ConfigureDevon4NetLogging(
    this LoggerConfiguration loggerConfiguration,
    IConfiguration configuration,
    List<ILogEventEnricher>? logEnrichers = default)
    {
        loggerConfiguration
            .ReadFrom.Configuration(configuration, new ConfigurationReaderOptions
            {
                SectionName = LoggingOptions.SectionName,
            })
            .ConfigureLogEnrichers(configuration)
            .ConfigureLogConsole(configuration)
            .ConfigureLogFile(configuration)
            .ConfigureLogGraylog(configuration)
            .ConfigureLogSeq(configuration)
            .ConfigureLogSqLiteDb(configuration);

        return loggerConfiguration;
    }
}