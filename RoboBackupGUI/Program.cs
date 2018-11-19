using System;
using System.Windows.Forms;

namespace RoboBackup {
    public static class Program {
        /// <summary>
        /// Main entry point to the GUI application.
        /// </summary>
        [STAThread]
        public static void Main() {
            Config.Load();
            Lang.SetLang();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
