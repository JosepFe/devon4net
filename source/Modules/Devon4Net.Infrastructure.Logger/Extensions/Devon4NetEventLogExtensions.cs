namespace Devon4Net.Infrastructure.Logger.Extensions;

using Microsoft.Extensions.Logging;

public static class Devon4NetEventLogExtensions
{
    public static void LogDevon4NetDebug(this ILogger logger, string message, params string?[] args) =>
        logger.Log(LogLevel.Debug, message, args);

    public static void LogDevon4NetInformation(this ILogger logger, string message, params string?[] args) =>
        logger.Log(LogLevel.Information, message, args);

    public static void LogDevon4NetWarning(this ILogger logger, Exception? exception, string message, params string?[] args) =>
        logger.Log(LogLevel.Warning, exception, message, args);

    public static void LogDevon4Error(this ILogger logger, Exception? exception, string message, params string?[] args) =>
        logger.Log(LogLevel.Error, exception, message, args);

    public static void LogDevon4Critical(this ILogger logger, Exception? exception, string message, params string?[] args) =>
        logger.Log(LogLevel.Critical, exception, message, args);
}
