using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace RoboBackup {
    /// <summary>
    /// Form with brief information about the RoboBackup application and its author.
    /// </summary>
    public partial class AboutForm : Form {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutForm"/> class.
        /// </summary>
        public AboutForm() {
            InitializeComponent();
            Localize();
            versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        }

        /// <summary>
        /// Translates the form strings to the language set by <see cref="Config.Language"/>
        /// </summary>
        private void Localize() {
            Text = Lang.Get("About");
            aboutVersionLabel.Text = Lang.Get("Version", ":");
            aboutAuthorLabel.Text = Lang.Get("Author", ":");
            aboutSourceCodeLabel.Text = Lang.Get("SourceCode", ":");
        }

        /// <summary>
        /// <see cref="sourceCodeLinkLabel.Click"/> event handler. Opens a GitHub link in default web browser.
        /// </summary>
        private void SourceCodeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(sourceCodeLinkLabel.Text);
        }
    }
}
