using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace RoboBackup {
    /// <summary>
    /// Application configuration for both GUI and service parts.
    /// </summary>
    public static class Config {
        /// <summary>
        /// Full path to the language directory.
        /// </summary>
        public static string LangRoot { get; private set; }

        /// <summary>
        /// ISO 639-1 code of the selected language.
        /// </summary>
        public static string Language { get; set; } = "en";

        /// <summary>
        /// Log retention set in days.
        /// </summary>
        public static ushort LogRetention { get; set; } = 30;

        /// <summary>
        /// Full path to the log directory.
        /// </summary>
        public static string LogRoot { get; private set; }

        /// <summary>
        /// Collection of all backup tasks. Key is the <see cref="Task.Guid"/>, Value is the <see cref="Task"/>.
        /// </summary>
        public static Dictionary<string, Task> Tasks { get; private set; } = new Dictionary<string, Task>();
        
        /// <summary>
        /// Full path to the configuration file.
        /// </summary>
        private static readonly string _configFile;

        /// <summary>
        /// XML Serializer for <see cref="SerializedConfig"/> serialization/deserialization for configuration saving/loading.
        /// </summary>
        private static XmlSerializer _xmlSerializer = new XmlSerializer(typeof(SerializedConfig));

        /// <summary>
        /// Static constructor to populate the file and directory paths according to the executable location.
        /// </summary>
        static Config() {
            string exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string progData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            _configFile = Path.Combine(progData, "RoboBackup/config.xml");
            LangRoot = Path.Combine(exeDir, "lang");
            LogRoot = Path.Combine(progData, "RoboBackup/logs");
            Directory.CreateDirectory(LogRoot);
        }

        /// <summary>
        /// Loads configuration from the default XML file.
        /// </summary>
        public static void Load() {
            Logger.LogConfigLoad();
            if (File.Exists(_configFile)) {
                using (StreamReader reader = new StreamReader(_configFile)) {
                    SerializedConfig serializedConfig = (SerializedConfig)_xmlSerializer.Deserialize(reader);
                    Language = serializedConfig.Language;
                    LogRetention = serializedConfig.LogRetention;
                    Tasks = serializedConfig.Tasks.ToDictionary(task => task.Guid, task => task);
                }
            }
            Logger.LogConfig();
        }

        /// <summary>
        /// saves configuration into the default XML file.
        /// </summary>
        public static void Save() {
            SerializedConfig serializedConfig = new SerializedConfig() { Language = Language, LogRetention = LogRetention, Tasks = Tasks.Values.ToArray() };
            using (StreamWriter writer = new StreamWriter(_configFile)) {
                _xmlSerializer.Serialize(writer, serializedConfig);
            }
        }

        /// <summary>
        /// Helper struct for XML serialization/deserialization.
        /// </summary>
        [Serializable]
        public struct SerializedConfig {
            /// <summary>
            /// ISO 639-1 code of the selected language.
            /// </summary>
            public string Language;

            /// <summary>
            /// Log retention set in days.
            /// </summary>
            public ushort LogRetention;

            /// <summary>
            /// Array of all backup tasks.
            /// </summary>
            public Task[] Tasks;
        }
    }
}
