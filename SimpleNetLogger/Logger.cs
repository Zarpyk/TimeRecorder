using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace SimpleNetLogger {
    public static class Logger {
        public static readonly List<LogListener> logListeners = new();

        public static void AddListener(LogListener listener) {
            logListeners.Add(listener);
        }

        public static void RemoveListener(LogListener listener) {
            logListeners.Remove(listener);
        }

        #region LogMethods
        #region Trace
        public static void Trace(string message) {
            Log(message, LogLevel.Trace);
        }

        public static void Trace(Exception ex) {
            Log(ex.ToString(), LogLevel.Trace);
        }
        #endregion

        #region Debug
        public static void Debug(string message) {
            Log(message, LogLevel.Debug);
        }

        public static void Debug(Exception ex) {
            Log(ex.ToString(), LogLevel.Debug);
        }
        #endregion

        #region Info
        public static void Info(string message) {
            Log(message, LogLevel.Info);
        }

        public static void Info(Exception ex) {
            Log(ex.ToString(), LogLevel.Info);
        }
        #endregion

        #region Warn
        public static void Warn(string message) {
            Log(message, LogLevel.Warn);
        }

        public static void Warn(Exception ex) {
            Log(ex.ToString(), LogLevel.Warn);
        }
        #endregion

        #region Error
        public static void Error(string message) {
            Log(message, LogLevel.Error);
        }

        public static void Error(Exception ex) {
            Log(ex.ToString(), LogLevel.Error);
        }
        #endregion

        #region Fatal
        public static void Fatal(string message) {
            Log(message, LogLevel.Fatal);
        }

        public static void Fatal(Exception ex) {
            Log(ex.ToString(), LogLevel.Fatal);
        }
        #endregion
        #endregion

        private static void Log(string message, LogLevel level) {
            if (logListeners == null || logListeners.Count == 0) return;
            StackFrame stackFrame = new StackTrace(true).GetFrame(2);
            MethodBase method = stackFrame?.GetMethod();
            string name = method?.ReflectedType?.FullName;
            int line = stackFrame?.GetFileLineNumber() ?? -1;
            string caller = $"{name}:{line}";
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (LogListener logListener in logListeners) {
                bool checkLevel = LogLevelUtils.CheckLevel(level, logListener.GetMinLevel(), logListener.GetMaxLevel());
                if (!checkLevel) continue;
                switch (level) {
                    case LogLevel.Trace:
                        logListener.Trace(message, caller, level);
                        break;
                    case LogLevel.Debug:
                        logListener.Debug(message, caller, level);
                        break;
                    case LogLevel.Info:
                        logListener.Info(message, caller, level);
                        break;
                    case LogLevel.Warn:
                        logListener.Warn(message, caller, level);
                        break;
                    case LogLevel.Error:
                        logListener.Error(message, caller, level);
                        break;
                    case LogLevel.Fatal:
                        logListener.Fatal(message, caller, level);
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(level), level, null);
                }
            }
        }
    }
}