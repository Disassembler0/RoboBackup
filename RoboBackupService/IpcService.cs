using System;

namespace RoboBackup {
    /// <summary>
    /// Service object class proxied via IPC from the server to the client.
    /// </summary>
    public class IpcService : MarshalByRefObject {
        /// <summary>
        /// Method to test availability od the IPC channel.
        /// </summary>
        /// <returns><see cref="String"/> "pong".</returns>
        public string Ping() {
            return "pong";
        }

        /// <summary>
        /// Reloads service configuration.
        /// </summary>
        public void ReloadConfig() {
            Logger.LogIpcReloadMessage();
            Config.Load();
            LogCleaner.Start();
        }

        /// <summary>
        /// Requests service to immediately run backup task.
        /// </summary>
        /// <param name="guid">GUID of the <see cref="Task"/> to be run.</param>
        public void RunTask(string guid) {
            Logger.LogIpcRunTaskMessage(guid);
            if (Config.Tasks.ContainsKey(guid)) {
                Task task = Config.Tasks[guid];
                task.Start();
            }
        }
    }
}
