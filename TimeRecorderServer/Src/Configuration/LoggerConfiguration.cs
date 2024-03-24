﻿using SimpleNetLogger;
using SimpleNetLogger.Listeners;

namespace TimeRecorderServer.Configuration {
    public static class LoggerConfiguration {
        public static void AddSimpleLogger(this ILoggingBuilder logging) {
            logging.ClearProviders();
            logging.AddConsole();
            Logger.AddListener(new ConsoleListener());
            // Logger.AddListener(new FileListener());
        }
    }
}