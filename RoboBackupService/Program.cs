using System.ServiceProcess;

namespace RoboBackup {
    public static class Program {
        /// <summary>
        /// Main entry point to the service application.
        /// </summary>
        public static void Main() {
            ServiceBase.Run(new Service());
        }
    }
}
