using System;
using System.IO;

namespace RoboBackup {
    /// <summary>
    /// Optional logging class for debugging purposes. Used when the <see cref="Service"/> is started with "/log" parameter.
    /// </summary>
    public static class Logger {
        /// <summary>
        /// Service log file stream writer.
        /// </summary>
        public static StreamWriter Writer { get; private set; }

        /// <summary>
        /// <see cref="DateTime"/> format used in logs.
        /// </summary>
        private const string _dateFormat = "yyyy-MM-dd HH:mm:ss.fff";

        /// <summary>
        /// Logs next run date of the <see cref="LogCleaner"/>.
        /// </summary>
        /// <param name="nextCleanupDate">Next run date of the <see cref="LogCleaner"/>.</param>
        public static void LogCleanupNextRunDate(DateTime nextCleanupDate) {
            if (Writer != null) {
                Log(string.Format("[Cleanup] Next log cleanup scheduled at {0}", nextCleanupDate.ToString(_dateFormat)));
            }
        }

        /// <summary>
        /// Logs start of <see cref="LogCleaner"/> deletion thread.
        /// </summary>
        public static void LogCleanupStart() {
            if (Writer != null) {
                Log("[Cleanup] Log cleanup started");
            }
        }

        /// <summary>
        /// Logs the values and tasks present in <see cref="Config"/>.
        /// </summary>
        public static void LogConfig() {
            if (Writer != null) {
                Log(string.Format("[Config] Got log retention period {0} days", Config.LogRetention));
                LogCleaner.RefreshNextRunDate(); // Produces log cleaner log line
                foreach (Task task in Config.Tasks.Values) {
                    task.RefreshNextRunDate(); // Produces task detail log line
                }
            }
        }

        /// <summary>
        /// Logs the beginning of <see cref="Config"/> file load.
        /// </summary>
        public static void LogConfigLoad() {
            if (Writer != null) {
                Log("[Config] Loading config");
            }
        }

        /// <summary>
        /// Logs deletion of old backup directory.
        /// </summary>
        /// <param name="directory">Deleted directory path.</param>
        public static void LogDirectoryDeleted(string directory) {
            if (Writer != null) {
                Log(string.Format("[Task] Deleted old directory {0}", directory));
            }
        }

        /// <summary>
        /// Logs the reception of <see cref="Config"/> reload message received via IPC.
        /// </summary>
        public static void LogIpcReloadMessage() {
            if (Writer != null) {
                Log("[Service] Received IPC request for config reload");
            }
        }

        /// <summary>
        /// Logs the reception of immediate <see cref="Task"/> run message received via IPC.
        /// </summary>
        /// <param name="guid">GUID of the <see cref="Task"/> to run.</param>
        public static void LogIpcRunTaskMessage(string guid) {
            if (Writer != null) {
                Log(string.Format("[Service] Received IPC request for task GUID {0} run", guid));
            }
        }

        /// <summary>
        /// Logs deletion of old log file.
        /// </summary>
        /// <param name="logFile">Deleted log file path.</param>
        public static void LogLogFileDeleted(string logFile) {
            if (Writer != null) {
                Log(string.Format("[Cleanup] Deleted old log file {0}", logFile));
            }
        }

        /// <summary>
        /// Logs the <see cref="Service"/> start
        /// </summary>
        public static void LogServiceStarted() {
            if (Writer != null) {
                Log("[Service] Service started");
            }
        }

        /// <summary>
        /// Logs the <see cref="Service"/> stop.
        /// </summary>
        public static void LogServiceStopped() {
            if (Writer != null) {
                Log("[Service] Service stopped");
            }
        }

        /// <summary>
        /// Logs next run date of a <see cref="Task"/>.
        /// </summary>
        /// <param name="task">The <see cref="Task"/> to be logged.</param>
        public static void LogTaskNextRunDate(Task task) {
            if (Writer != null) {
                Log(string.Format("[Task] Next task GUID {0} run scheduled at {1}", task.Guid, task.NextRunDate.ToString(_dateFormat)));
            }
        }

        /// <summary>
        /// Logs the start of the <see cref="Task"/> run thread.
        /// </summary>
        /// <param name="task">The starting <see cref="Task"/>.</param>
        public static void LogTaskStart(Task task) {
            if (Writer != null) {
                Log(string.Format("[Task] Task GUID {0} started", task.Guid));
            }
        }

        /// <summary>
        /// Logs <see cref="Service"/> timer next tick delay.
        /// </summary>
        /// <param name="delay">The delay of the next tick in milliseconds.</param>
        public static void LogTickDelay(int delay) {
            if (Writer != null) {
                Log(string.Format("[Timer] Next tick in {0} ms", delay));
            }
        }

        /// <summary>
        /// Logs the beginning of the <see cref="Service"/> timer tick.
        /// </summary>
        public static void LogTick() {
            if (Writer != null) {
                Log("[Timer] Tick");
            }
        }

        /// <summary>
        /// Logs <see cref="Service"/> timer tick skew warning.
        /// </summary>
        public static void LogTickSkew() {
            if (Writer != null) {
                Log("[Timer] Last tick more than 90 seconds ago or in the future, refreshing schedules");
            }
        }

        /// <summary>
        /// Opens a <see cref="StreamWriter"/> for the given log file name.
        /// </summary>
        /// <param name="path">Log file name.</param>
        public static void Open(string path) {
            Writer = new StreamWriter(path, true) { AutoFlush = true };
        }

        /// <summary>
        /// Opens a <see cref="StreamWriter"/> for the given <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to write into.</param>
        public static void Open(Stream stream) {
            Writer = new StreamWriter(stream) { AutoFlush = true };
        }

        /// <summary>
        /// Logs a message using <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        private static void Log(string message) {
            Writer.WriteLine(string.Format("{0} - {1}", DateTime.Now.ToString(_dateFormat), message));
        }
    }
}
