using SimpleNetLogger;
using SimpleNetLogger.Listeners;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace TimeRecorderAPI.Configuration {
    public static class LoggerConfiguration {
        public static void AddSimpleLogger(this ILoggingBuilder logging) {
            logging.ClearProviders();
            logging.AddConsole();
            logging.AddFilter("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", LogLevel.None);
            Logger.AddListener(new ConsoleListener());
            // Logger.AddListener(new FileListener());
        }
    }
}