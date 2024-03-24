using System;

namespace SimpleNetLogger {
    public abstract class LogListener {
        protected string format { get; set; } = "[{time}] [{caller}] [{level}] {message}";
        protected LogLevel minLevel { get; set; } = LogLevel.Trace;
        protected LogLevel maxLevel { get; set; } = LogLevel.Fatal;

        public void SetFormat(string format) {
            this.format = format;
        }

        public void SetMinLevel(LogLevel minLevel) {
            this.minLevel = minLevel;
        }

        public void SetMaxLevel(LogLevel maxLevel) {
            this.maxLevel = maxLevel;
        }

        public void SetMinMaxLevel(LogLevel level) {
            SetMinLevel(level);
            SetMaxLevel(level);
        }

        public void SetMinMaxLevel(LogLevel minLevel, LogLevel maxLevel) {
            SetMinLevel(minLevel);
            SetMaxLevel(maxLevel);
        }

        internal void Trace(string message, string caller, LogLevel level) {
            Log(message, caller, level);
        }

        internal void Debug(string message, string caller, LogLevel level) {
            Log(message, caller, level);
        }

        internal void Info(string message, string caller, LogLevel level) {
            Log(message, caller, level);
        }

        internal void Warn(string message, string caller, LogLevel level) {
            Log(message, caller, level);
        }

        internal void Error(string message, string caller, LogLevel level) {
            Log(message, caller, level);
        }

        internal void Fatal(string message, string caller, LogLevel level) {
            Log(message, caller, level);
        }

        protected abstract void Log(string message, string caller, LogLevel level);

        protected virtual string GetMessage(string message, string caller, LogLevel level) {
            return format.Replace("{message}", message).Replace("{caller}", caller)
                         .Replace("{time}", DateTime.Now.ToString("HH:mm:ss.ffffff"))
                         .Replace("{level}", level.ToString());
        }

        internal LogLevel GetMinLevel() {
            return minLevel;
        }

        internal LogLevel GetMaxLevel() {
            return maxLevel;
        }
    }
}