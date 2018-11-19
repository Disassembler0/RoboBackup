using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RoboBackup {
    /// <summary>
    /// Form to create or edit backup tasks details.
    /// </summary>
    public partial class TaskForm : Form {
        /// <summary>
        /// <see cref="Task"/> returned by successfully completed form.
        /// </summary>
        public Task ResultTask { get; private set; }

        /// <summary>
        /// <see cref="Task.Guid"/> to be reused when updating a <see cref="Task"/>.
        /// </summary>
        private string _guid;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskForm"/> class.
        /// </summary>
        public TaskForm() {
            InitializeComponent();
            Localize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskForm"/> class and populates form controls with the <see cref="Task"/> properties.
        /// </summary>
        /// <param name="task"><see cref="Task"/> to be edited.</param>
        public TaskForm(Task task) : this() {
            _guid = task.Guid;
            titleTextBox.Text = task.Title;
            sourceFolderBrowserDialog.SelectedPath = sourceTextBox.Text = task.Source;
            PopulateSourceTreeView();
            foreach (string path in task.ExcludedDirs) {
                TreeNode node = DirectoryTree.FindNode(sourceTreeView.Nodes, path);
                if (node != null) {
                    node.Checked = false;
                }
            }
            foreach (string path in task.ExcludedFiles) {
                TreeNode node = DirectoryTree.FindNode(sourceTreeView.Nodes, path);
                if (node != null) {
                    node.Checked = false;
                }
            }
            destinationFolderBrowserDialog.SelectedPath = destinationTextBox.Text = task.Destination;
            if (!string.IsNullOrEmpty(task.Credential)) {
                usernameTextBox.Text = Lang.Get("ReenterCredentials");
            }
            if (task.Method == Method.Differential) {
                differentialRadioButton.Checked = true;
                retentionNumericUpDown.Value = task.Retention;
            } else if (task.Method == Method.Full) {
                fullRadioButton.Checked = true;
                retentionNumericUpDown.Value = task.Retention;
            }
            if (task.Period == Period.Weekly) {
                weekdayComboBox.SelectedIndex = (int)task.DayOfWeek;
            } else if (task.Period == Period.Monthly) {
                monthdayNumericUpDown.Value = task.DayOfMonth;
            }
            timeDateTimePicker.Value = DateTime.Today.AddHours(task.Hour).AddMinutes(task.Minute); ;
        }

        /// <summary>
        /// Enables or disables network credentials controls based on the source and destination directory UNC conformance.
        /// </summary>
        private void CheckUncPath() {
            bool unc = Unc.IsUncPath(sourceTextBox.Text) || Unc.IsUncPath(destinationTextBox.Text);
            usernameTextBox.Enabled = unc;
            passwordTextBox.Enabled = unc;
            if (!unc) {
                usernameTextBox.Text = string.Empty;
                passwordTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Collects form control values and creates a <see cref="ResultTask"/> form result.
        /// </summary>
        private void CreateTask() {
            _guid = _guid ?? Guid.NewGuid().ToString();
            Task task = new Task() { Guid = _guid, Title = titleTextBox.Text, Source = sourceTextBox.Text, Destination = destinationTextBox.Text };
            List<TreeNode> excludedNodes = DirectoryTree.GetExcludedNodes(sourceTreeView.Nodes);
            List<string> excludedDirs = new List<string>();
            List<string> excludedFiles = new List<string>();
            foreach (TreeNode node in excludedNodes) {
                if ((EntryType)node.Tag == EntryType.Directory) {
                    excludedDirs.Add(node.FullPath);
                } else {
                    excludedFiles.Add(node.FullPath);
                }
            }
            task.ExcludedDirs = excludedDirs.ToArray();
            task.ExcludedFiles = excludedFiles.ToArray();
            task.Credential = Credential.Encrypt(_guid, new Credential() { Username = usernameTextBox.Text, Password = passwordTextBox.Text });
            if (incrementalRadioButton.Checked) {
                task.Method = Method.Incremental;
                task.Retention = 1;
            } else if (differentialRadioButton.Checked) {
                task.Method = Method.Differential;
                task.Retention = (ushort)retentionNumericUpDown.Value;
            } else {
                task.Method = Method.Full;
                task.Retention = (ushort)retentionNumericUpDown.Value;
            }
            if (dailyRadioButton.Checked) {
                task.Period = Period.Daily;
            } else if (weeklyRadioButton.Checked) {
                task.Period = Period.Weekly;
                task.DayOfWeek = (DayOfWeek)weekdayComboBox.SelectedIndex;
            } else {
                task.Period = Period.Monthly;
                task.DayOfMonth = (byte)monthdayNumericUpDown.Value;
            }
            task.Hour = (byte)timeDateTimePicker.Value.Hour;
            task.Minute = (byte)timeDateTimePicker.Value.Minute;
            ResultTask = task;
        }

        /// <summary>
        /// <see cref="destinationButton.Click"/> event handler. Displays destination folder selection dialog.
        /// </summary>
        private void DestinationButton_Click(object sender, EventArgs e) {
            if (destinationFolderBrowserDialog.ShowDialog() == DialogResult.OK) {
                destinationTextBox.Text = destinationFolderBrowserDialog.SelectedPath = Unc.TranslatePath(destinationFolderBrowserDialog.SelectedPath);
            }
        }

        /// <summary>
        /// <see cref="differentialRadioButton.CheckedChanged"/> event handler. Enables <see cref="retentionNumericUpDown"/> control.
        /// </summary>
        private void DifferentialRadioButton_CheckedChanged(object sender, EventArgs e) {
            retentionNumericUpDown.Enabled = true;
        }

        /// <summary>
        /// <see cref="destinationTextBox.TextChanged"/> event handler. Enables or disables network credentials input fields.
        /// </summary>
        private void DestinationTextBox_TextChanged(object sender, EventArgs e) {
            CheckUncPath();
        }

        /// <summary>
        /// <see cref="fullRadioButton.CheckedChanged"/> event handler. Enables <see cref="retentionNumericUpDown"/> control.
        /// </summary>
        private void FullRadioButton_CheckedChanged(object sender, EventArgs e) {
            retentionNumericUpDown.Enabled = true;
        }

        /// <summary>
        /// <see cref="incrementalRadioButton.CheckedChanged"/> event handler. Disables <see cref="retentionNumericUpDown"/> control.
        /// </summary>
        private void IncrementalRadioButton_CheckedChanged(object sender, EventArgs e) {
            retentionNumericUpDown.Enabled = false;
        }

        /// <summary>
        /// Translates the form strings to the language set by <see cref="Config.Language"/>.
        /// </summary>
        private void Localize() {
            Text = Lang.Get("Task");
            titleLabel.Text = Lang.Get("Title", ":");
            directoriesGroupBox.Text = Lang.Get("Directories");
            sourceLabel.Text = Lang.Get("Source", ":");
            destinationLabel.Text = Lang.Get("Destination", ":");
            networkCredentialsGroupBox.Text = Lang.Get("NetworkCredentials");
            usernameLabel.Text = Lang.Get("Username", ":");
            passwordLabel.Text = Lang.Get("Password", ":");
            methodGroupBox.Text = Lang.Get("Method");
            incrementalRadioButton.Text = Lang.Get("Incremental");
            differentialRadioButton.Text = Lang.Get("Differential");
            fullRadioButton.Text = Lang.Get("Full");
            retentionLabel.Text = Lang.Get("Retention", ":");
            scheduleGroupBox.Text = Lang.Get("Schedule");
            dailyRadioButton.Text = Lang.Get("Daily");
            weeklyRadioButton.Text = Lang.Get("DayOfWeek", ":");
            monthlyRadioButton.Text = Lang.Get("DayOfMonth", ":");
            timeLabel.Text = Lang.Get("AtTime", ":");
            saveTaskButton.Text = Lang.Get("SaveTask");
            foreach (DayOfWeek dow in Enum.GetValues(typeof(DayOfWeek))) {
                weekdayComboBox.Items.Add(Lang.Get(dow.ToString()));
            }
            weekdayComboBox.Text = Lang.Get(DayOfWeek.Sunday.ToString());
            dailyRadioButton.Checked = true;
        }

        /// <summary>
        /// <see cref="methodHelpButton.Click"/> event handler. Displays a <see cref="MessageBox"/> with backup methods explanation.
        /// </summary>
        private void MethodHelpButton_Click(object sender, EventArgs e) {
            string message = string.Format("{0}\n\n{1}\n\n{2}\n\n{3}", Lang.Get("HelpIncremental"), Lang.Get("HelpDifferential"), Lang.Get("HelpFull"), Lang.Get("HelpRetention"));
            MessageBox.Show(message, Lang.Get("Help"), MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        /// <summary>
        /// monthdayNumericUpDown.ValueChanged event handler. Switches schedule selection to "monthly" when the day of month value changes.
        /// </summary>
        private void MonthdayNumericUpDown_ValueChanged(object sender, EventArgs e) {
            monthlyRadioButton.Checked = true;
        }

        /// <summary>
        /// Populates the <see cref="sourceTreeView"/> with nodes corresponding to filesystem objects located under the directory selected by <see cref="sourceFolderBrowserDialog"/>.
        /// </summary>
        private void PopulateSourceTreeView() {
            sourceTreeView.Nodes.Clear();
            sourceTreeView.Nodes.AddRange(DirectoryTree.LoadNodes(sourceFolderBrowserDialog.SelectedPath));
        }

        /// <summary>
        /// saveTaskButton.Click event handler. Populates <see cref="ResultTask"/> and closes the form.
        /// </summary>
        private void SaveTaskButton_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(titleTextBox.Text) || string.IsNullOrEmpty(sourceTextBox.Text) || string.IsNullOrEmpty(destinationTextBox.Text)) {
                MessageBox.Show(Lang.Get("IncompleteTaskForm"), Lang.Get("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CreateTask();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// <see cref="sourceButton.Click"/> event handler. Displays source folder selection dialog.
        /// </summary>
        private void SourceButton_Click(object sender, EventArgs e) {
            if (sourceFolderBrowserDialog.ShowDialog() == DialogResult.OK) {
                sourceTextBox.Text = sourceFolderBrowserDialog.SelectedPath = Unc.TranslatePath(sourceFolderBrowserDialog.SelectedPath);
                PopulateSourceTreeView();
            }
        }

        /// <summary>
        /// <see cref="sourceTextBox.TextChanged"/> event handler. Enables or disables network credentials input fields.
        /// </summary>
        private void SourceTextBox_TextChanged(object sender, EventArgs e) {
            CheckUncPath();
        }

        /// <summary>
        /// <see cref="sourceTreeView.AfterCheck"/> event handler. Toggles <see cref="TreeNode.Checked"/> for the <see cref="TreeNode.Nodes"/> of the node which received the click.
        /// </summary>
        private void SourceTreeView_AfterCheck(object sender, TreeViewEventArgs e) {
            DirectoryTree.ToggleChildren(e.Node, e.Node.Checked);
        }

        /// <summary>
        /// <see cref="weekdayComboBox.SelectedIndexChanged"/> event handler. Switches schedule selection to "weekly" when the day of week value changes.
        /// </summary>
        private void WeekdayComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            weeklyRadioButton.Checked = true;
        }
    }
}
