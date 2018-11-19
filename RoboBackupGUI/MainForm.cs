using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace RoboBackup {
    /// <summary>
    /// Main form with overview of scheduled backup tasks.
    /// </summary>
    public partial class MainForm : Form {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm() {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            Localize();
            RedrawTasks();
        }

        /// <summary>
        /// Formats and localizes a <see cref="Task"/> to fit <see cref="ListViewItem"/>.
        /// </summary>
        /// <param name="task"><see cref="Task"/> to be formatted.</param>
        /// <returns>Localized <see cref="ListViewItem"/> representation of the <see cref="Task"/>.</returns>
        private static ListViewItem DrawTask(Task task) {
            string method = Lang.Get("IncrementalAbbr");
            if (task.Method == Method.Differential) {
                method = Lang.Get("DifferentialAbbr");
            } else if (task.Method == Method.Full) {
                method = Lang.Get("FullAbbr");
            }
            string schedule = Lang.Get("DailyAbbr");
            if (task.Period == Period.Weekly) {
                schedule = string.Format("{0}, {1}", Lang.Get("WeeklyAbbr"), Lang.Get(task.DayOfWeek.ToString()));
            } else if (task.Period == Period.Monthly) {
                schedule = string.Format("{0}, {1}.", Lang.Get("MonthlyAbbr"), task.DayOfMonth);
            }
            schedule = string.Format("{0}, {1} @ {2:D2}:{3:D2}", method, schedule, task.Hour, task.Minute);
            return new ListViewItem(new string[] { task.Title, schedule, task.Source, task.Destination }) { Tag = task.Guid };
        }

        /// <summary>
        /// Saves the configuration to file and sends a request for conf reload to the service via <see cref="IpcClient"/>.
        /// </summary>
        private static void SaveConfig() {
            Config.Save();
            try {
                IpcClient.GetService().ReloadConfig();
            } catch {
                MessageBox.Show(Lang.Get("UnableToConnectService"), Lang.Get("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// <see cref="aboutToolStripButton.Click"/> event handler. Opens a new <see cref="AboutForm"/> window.
        /// </summary>
        private void AboutToolStripButton_Click(object sender, EventArgs e) {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        /// <summary>
        /// <see cref="deleteTaskToolStripButton.Click"/> event handler. Removes a task from the configuration.
        /// </summary>
        private void DeleteTaskToolStripButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show(Lang.Get("DeleteConfirmation"), Lang.Get("Question"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                string guid = (string)taskListView.SelectedItems[0].Tag;
                Config.Tasks.Remove(guid);
                SaveConfig();
                RedrawTasks();
            }
        }

        /// <summary>
        /// <see cref="editTaskToolStripButton.Click"/> event handler. Opens a new <see cref="TaskForm"/> and updates the selected task in the configuration.
        /// </summary>
        private void EditTaskToolStripButton_Click(object sender, EventArgs e) {
            string guid = (string)taskListView.SelectedItems[0].Tag;
            TaskForm editForm = new TaskForm(Config.Tasks[guid]);
            if (editForm.ShowDialog() == DialogResult.OK) {
                Config.Tasks[guid] = editForm.ResultTask;
                SaveConfig();
                RedrawTasks();
            }
        }

        /// <summary>
        /// Translates the form strings to the language set by <see cref="Config.Language"/>.
        /// </summary>
        private void Localize() {
            newTaskToolStripButton.Text = Lang.Get("NewTask");
            editTaskToolStripButton.Text = Lang.Get("EditTask");
            deleteTaskToolStripButton.Text = Lang.Get("DeleteTask");
            showTaskLogsToolStripButton.Text = Lang.Get("ShowTaskLogs");
            runTaskToolStripButton.Text = Lang.Get("RunTask");
            settingsToolStripButton.Text = Lang.Get("Settings");
            aboutToolStripButton.Text = Lang.Get("About");
            titleColumnHeader.Text = Lang.Get("Title");
            scheduleColumnHeader.Text = Lang.Get("Schedule");
            sourceColumnHeader.Text = Lang.Get("Source");
            destinationColumnHeader.Text = Lang.Get("Destination");
        }

        /// <summary>
        /// <see cref="newTaskToolStripButton.Click"/> event handler. Opens a new <see cref="TaskForm"/> and inserts the new task into the configuration.
        /// </summary>
        private void NewTaskToolStripButton_Click(object sender, EventArgs e) {
            TaskForm newForm = new TaskForm();
            if (newForm.ShowDialog() == DialogResult.OK) {
                Task t = newForm.ResultTask;
                Config.Tasks[t.Guid] = t;
                SaveConfig();
                RedrawTasks();
            }
        }

        /// <summary>
        /// Redraws <see cref="taskListView"/> items using the current locale set by <see cref="Config.Language"/>.
        /// </summary>
        private void RedrawTasks() {
            taskListView.Items.Clear();
            foreach (Task task in Config.Tasks.Values) {
                taskListView.Items.Add(DrawTask(task));
            }
            taskListView.SelectedIndices.Clear();
            TaskListView_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// <see cref="runTaskToolStripButton.Click"/> event handler. Sends a request for immediate backup start to the service via <see cref="IpcClient"/>.
        /// </summary>
        private void RunTaskToolStripButton_Click(object sender, EventArgs e) {
            string guid = (string)taskListView.SelectedItems[0].Tag;
            try {
                IpcClient.GetService().RunTask(guid);
                MessageBox.Show(Lang.Get("BackupStarted"), Lang.Get("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch {
                MessageBox.Show(Lang.Get("UnableToConnectService"), Lang.Get("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// <see cref="settingsToolStripButton.Click"/> event handler. Opens a <see cref="SettingsForm"/> and updates the configuration for log retention and language.
        /// </summary>
        private void SettingsToolStripButton_Click(object sender, EventArgs e) {
            SettingsForm settings = new SettingsForm();
            if (settings.ShowDialog() == DialogResult.OK) {
                bool langChanged = settings.Language != Config.Language;
                Config.LogRetention = settings.LogRetention;
                Config.Language = settings.Language;
                SaveConfig();
                if (langChanged) {
                    Lang.SetLang();
                    Localize();
                    RedrawTasks();
                }
            }
        }

        /// <summary>
        /// <see cref="showTaskLogsToolStripButton.Click"/> handler. Opens a directory with logs for the selected <see cref="Task"/>.
        /// </summary>
        private void ShowTaskLogsToolStripButton_Click(object sender, EventArgs e) {
            string guid = (string)taskListView.SelectedItems[0].Tag;
            string logDir = Path.Combine(Config.LogRoot, guid);
            if (!Directory.Exists(logDir)) {
                MessageBox.Show(Lang.Get("NoTaskLogs"), Lang.Get("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Process.Start(logDir);
        }

        /// <summary>
        /// <see cref="taskListView.MouseDoubleClick"/> event handler. Calls <see cref="EditTaskToolStripButton_Click"/>.
        /// </summary>
        private void TaskListView_MouseDoubleClick(object sender, MouseEventArgs e) {
            EditTaskToolStripButton_Click(sender, null);
        }

        /// <summary>
        /// <see cref="taskListView.SelectedIndexChanged"/> event handler. Changes the state of the <see cref="toolStrip"/> buttons according to the <see cref="taskListView"/> selection.
        /// </summary>
        private void TaskListView_SelectedIndexChanged(object sender, EventArgs e) {
            bool selected = taskListView.SelectedItems.Count != 0;
            editTaskToolStripButton.Enabled = selected;
            deleteTaskToolStripButton.Enabled = selected;
            showTaskLogsToolStripButton.Enabled = selected;
            runTaskToolStripButton.Enabled = selected;
        }
    }
}
