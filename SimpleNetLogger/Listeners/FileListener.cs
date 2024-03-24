using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Timers;

namespace SimpleNetLogger.Listeners {
    public class FileListener : LogListener {
        private string _initialPath;
        private string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                            "SimpleNetLogger");
        private string _filePrefix = "";
        private int maxLogs = 5;

        private Timer checkDayTimer;
        private bool changingDay = false;

        private StreamWriter _writer;

        public FileListener() {
            _initialPath = _path;
        }

        public void SetFilePath(string path, string filePrefix = "") {
            _path = path;
            _initialPath = _path;
            _filePrefix = filePrefix;
            _path = Path.Combine(_initialPath, _filePrefix,
                                 (string.IsNullOrWhiteSpace(_filePrefix) ? "" : _filePrefix + "-") +
                                 DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            CheckPath();
        }

        public void SetMaxLogs(int maxLogs) {
            this.maxLogs = maxLogs;
        }

        public void SetCheckNewDay(bool checkNewDay, int checkDayInterval = 1000) {
            if (checkNewDay) {
                checkDayTimer = new Timer();
                checkDayTimer.Interval = checkDayInterval;
                checkDayTimer.Elapsed -= CheckDay;
                checkDayTimer.Elapsed += CheckDay;
                checkDayTimer.AutoReset = true;
                checkDayTimer.Start();
            } else {
                if (checkDayTimer == null) return;
                checkDayTimer.Elapsed -= CheckDay;
                checkDayTimer.Stop();
                checkDayTimer.Dispose();
            }
        }

        private void CheckDay(object o, ElapsedEventArgs elapsedEventArgs) {
            int day = DateTime.Parse(Path.GetFileNameWithoutExtension(_path)?.Replace(_filePrefix + "-", "") ??
                                     DateTime.Now.ToString(CultureInfo.InvariantCulture)).Day;
            if (DateTime.Now.Day != day && !changingDay) {
                changingDay = true;
                _path = Path.Combine(_initialPath, _filePrefix,
                                     (string.IsNullOrWhiteSpace(_filePrefix) ? "" : _filePrefix + "-") +
                                     DateTime.Now.ToString("yyyy-MM-dd") + ".log");
                CheckPath();
                changingDay = false;
            }
        }

        private void CheckPath() {
            string directory;
            try {
                directory = Path.GetDirectoryName(_path);
            } catch (Exception e) {
                Logger.Fatal(e);
                throw;
            }
            if (directory == null) return;
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(_path)) {
                using (File.Create(_path)) { }
            }
            if(_writer != null) {
                _writer.Close();
                _writer.Dispose();
            }
            _writer = new StreamWriter(_path, true);
            _writer.AutoFlush = true;

            RemoveOldLogs();
        }

        private void RemoveOldLogs() {
            string directoryName = Path.GetDirectoryName(_path);
            if (Directory.Exists(directoryName)) {
                List<string> files = Directory.GetFiles(directoryName).ToList();
                if (files.Count <= maxLogs) return;
                try {
                    files.Sort((a, b) => DateTime
                                        .Parse(Path.GetFileNameWithoutExtension(a.Replace(_filePrefix + "-", "")))
                                        .CompareTo(DateTime.Parse(Path.GetFileNameWithoutExtension(b
                                                                     .Replace(_filePrefix + "-", "")))));
                } catch (Exception e) {
                    Logger.Error(e);
                }

                for (int i = 0; i < files.Count - maxLogs; i++) {
                    try {
                        File.Delete(files[i]);
                    } catch (Exception e) {
                        Logger.Error(e);
                    }
                }
            }
        }

        protected override void Log(string message, string caller, LogLevel level) {
            string logMessage = GetMessage(message, caller, level);
            WriteToFile(logMessage);
        }

        private void WriteToFile(string logMessage) {
            lock (_writer) {
                _writer.WriteLine(logMessage);
            }
        }
    }
}