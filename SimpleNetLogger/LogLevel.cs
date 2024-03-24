namespace SimpleNetLogger {
    internal static class LogLevelUtils {
        internal static bool CheckLevel(LogLevel level, LogLevel min, LogLevel max) {
            return level >= min && level <= max;
        }
    }

    public enum LogLevel {
        Trace = 0, Debug = 1, Info = 2, Warn = 3,
        Error = 4, Fatal = 5
    }
}