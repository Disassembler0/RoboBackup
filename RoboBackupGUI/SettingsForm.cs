using System.Linq;
using System.Windows.Forms;

namespace RoboBackup {
    /// <summary>
    /// Form with global settings - language and log retention
    /// </summary>
    public partial class SettingsForm : Form {
        /// <summary>
        /// Language returned by successfully completed form.
        /// </summary>
        public string Language { get; private set; }

        /// <summary>
        /// Log retention in days returned by successfully completed form.
        /// </summary>
        public ushort LogRetention { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsForm"/> class.
        /// </summary>
        public SettingsForm() {
            InitializeComponent();
            Localize();
            languageComboBox.Items.AddRange(Lang.AvailableLocales.Keys.ToArray());
            languageComboBox.Text = Lang.Get(""); // Name of the language is stored in the empty string key
            logRetentionNumericUpDown.Value = Config.LogRetention;
        }

        /// <summary>
        /// Translates the form strings to the language set by <see cref="Config.Language"/>.
        /// </summary>
        private void Localize() {
            Text = Lang.Get("Settings");
            languageLabel.Text = Lang.Get("Language", ":");
            logRetentionLabel.Text = Lang.Get("LogRetention", ":");
            saveSettingsButton.Text = Lang.Get("SaveSettings");
        }

        /// <summary>
        /// <see cref="saveSettingsButton.Click"/> event handler. Populates <see cref="Language"/> and <see cref="LogRetention"/> and closes the form.
        /// </summary>
        private void SaveSettingsButton_Click(object sender, System.EventArgs e) {
            Language = Lang.AvailableLocales[languageComboBox.Text];
            LogRetention = (ushort)logRetentionNumericUpDown.Value;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
