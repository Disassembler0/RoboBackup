using System;
using System.Threading;

namespace RoboBackup {
    /// <summary>
    /// Backup log file cleaner class.
    /// </summary>
    public static class LogCleaner {
        /// <summary>
        /// <see cref="DateTime"/> of the next log file deletion run.
        /// </summary>
        public static DateTime NextRunDate {
            get {
                if (_nextRunDate == DateTime.MinValue) {
                    RefreshNextRunDate();
                }
                return _nextRunDate;
            }
        }

        /// <summary>
        /// <see cref="Thread"/> used to execute the log file deletion process.
        /// </summary>
        private static Thread _cleanupThread;

        /// <summary>
        /// Private variable backing the <see cref="NextRunDate"/> property.
        /// </summary>
        private static DateTime _nextRunDate;

        /// <summary>
        /// Calculates <see cref="DateTime"/> of the next log file deletion run.
        /// </summary>
        public static void RefreshNextRunDate() {
            _nextRunDate = DateTime.Today.AddDays(1);
            Logger.LogCleanupNextRunDate(_nextRunDate);
        }

        /// <summary>
        /// Starts the file deletion in a separate thread.
        /// </summary>
        public static void Start() {
            Logger.LogCleanupStart();
            _cleanupThread = new Thread(new ThreadStart(SysUtils.DeleteOldLogs));
            _cleanupThread.Start();
        }
    }
}
