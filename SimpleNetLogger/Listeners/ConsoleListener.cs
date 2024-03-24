using System;

namespace SimpleNetLogger.Listeners {
    public class ConsoleListener : LogListener {
        protected override void Log(string message, string caller, LogLevel level) {
            string logMessage = GetMessage(message, caller, level);
            Console.WriteLine(logMessage);
        }
    }
}