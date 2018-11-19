using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace RoboBackup {
    /// <summary>
    /// <see cref="Service"/> installer used by the Setup project.
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer {
        public ProjectInstaller() {
            InitializeComponent();
        }

        /// <summary>
        /// <see cref="serviceInstaller.AfterInstall"/> event hadler. Starts the installed service.
        /// </summary>
        private void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e) {
            using (ServiceController serviceController = new ServiceController(serviceInstaller.ServiceName)) {
                serviceController.Start();
            }
        }
    }
}
