namespace RoboBackup
{
    partial class TaskForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.titleLabel = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.fullRadioButton = new System.Windows.Forms.RadioButton();
            this.incrementalRadioButton = new System.Windows.Forms.RadioButton();
            this.sourceTreeView = new System.Windows.Forms.TreeView();
            this.sourceTextBox = new System.Windows.Forms.TextBox();
            this.sourceButton = new System.Windows.Forms.Button();
            this.destinationTextBox = new System.Windows.Forms.TextBox();
            this.destinationButton = new System.Windows.Forms.Button();
            this.differentialRadioButton = new System.Windows.Forms.RadioButton();
            this.retentionLabel = new System.Windows.Forms.Label();
            this.retentionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.directoriesGroupBox = new System.Windows.Forms.GroupBox();
            this.methodGroupBox = new System.Windows.Forms.GroupBox();
            this.methodHelpButton = new System.Windows.Forms.Button();
            this.saveTaskButton = new System.Windows.Forms.Button();
            this.scheduleGroupBox = new System.Windows.Forms.GroupBox();
            this.timeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.timeLabel = new System.Windows.Forms.Label();
            this.monthdayNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.dailyRadioButton = new System.Windows.Forms.RadioButton();
            this.weeklyRadioButton = new System.Windows.Forms.RadioButton();
            this.weekdayComboBox = new System.Windows.Forms.ComboBox();
            this.monthlyRadioButton = new System.Windows.Forms.RadioButton();
            this.networkCredentialsGroupBox = new System.Windows.Forms.GroupBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.sourceFolderBrowserDialog = new RoboBackup.OpenFolderDialog();
            this.destinationFolderBrowserDialog = new RoboBackup.OpenFolderDialog();
            ((System.ComponentModel.ISupportInitialize)(this.retentionNumericUpDown)).BeginInit();
            this.directoriesGroupBox.SuspendLayout();
            this.methodGroupBox.SuspendLayout();
            this.scheduleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthdayNumericUpDown)).BeginInit();
            this.networkCredentialsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(18, 15);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(30, 13);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Title:";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(87, 12);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(262, 20);
            this.titleTextBox.TabIndex = 1;
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(6, 22);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(44, 13);
            this.sourceLabel.TabIndex = 0;
            this.sourceLabel.Text = "Source:";
            // 
            // destinationLabel
            // 
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Location = new System.Drawing.Point(6, 173);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(63, 13);
            this.destinationLabel.TabIndex = 4;
            this.destinationLabel.Text = "Destination:";
            // 
            // fullRadioButton
            // 
            this.fullRadioButton.AutoSize = true;
            this.fullRadioButton.Location = new System.Drawing.Point(6, 72);
            this.fullRadioButton.Name = "fullRadioButton";
            this.fullRadioButton.Size = new System.Drawing.Size(41, 17);
            this.fullRadioButton.TabIndex = 2;
            this.fullRadioButton.Text = "Full";
            this.fullRadioButton.UseVisualStyleBackColor = true;
            this.fullRadioButton.CheckedChanged += new System.EventHandler(this.FullRadioButton_CheckedChanged);
            // 
            // incrementalRadioButton
            // 
            this.incrementalRadioButton.AutoSize = true;
            this.incrementalRadioButton.Checked = true;
            this.incrementalRadioButton.Location = new System.Drawing.Point(6, 20);
            this.incrementalRadioButton.Name = "incrementalRadioButton";
            this.incrementalRadioButton.Size = new System.Drawing.Size(80, 17);
            this.incrementalRadioButton.TabIndex = 0;
            this.incrementalRadioButton.TabStop = true;
            this.incrementalRadioButton.Text = "Incremental";
            this.incrementalRadioButton.UseVisualStyleBackColor = true;
            this.incrementalRadioButton.CheckedChanged += new System.EventHandler(this.IncrementalRadioButton_CheckedChanged);
            // 
            // sourceTreeView
            // 
            this.sourceTreeView.CheckBoxes = true;
            this.sourceTreeView.FullRowSelect = true;
            this.sourceTreeView.Location = new System.Drawing.Point(75, 45);
            this.sourceTreeView.Name = "sourceTreeView";
            this.sourceTreeView.Size = new System.Drawing.Size(255, 119);
            this.sourceTreeView.TabIndex = 3;
            this.sourceTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.SourceTreeView_AfterCheck);
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.sourceTextBox.Location = new System.Drawing.Point(75, 19);
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.ReadOnly = true;
            this.sourceTextBox.Size = new System.Drawing.Size(224, 20);
            this.sourceTextBox.TabIndex = 1;
            this.sourceTextBox.TextChanged += new System.EventHandler(this.SourceTextBox_TextChanged);
            // 
            // sourceButton
            // 
            this.sourceButton.Location = new System.Drawing.Point(305, 19);
            this.sourceButton.Name = "sourceButton";
            this.sourceButton.Size = new System.Drawing.Size(25, 21);
            this.sourceButton.TabIndex = 2;
            this.sourceButton.Text = "...";
            this.sourceButton.UseVisualStyleBackColor = true;
            this.sourceButton.Click += new System.EventHandler(this.SourceButton_Click);
            // 
            // destinationTextBox
            // 
            this.destinationTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.destinationTextBox.Location = new System.Drawing.Point(75, 170);
            this.destinationTextBox.Name = "destinationTextBox";
            this.destinationTextBox.ReadOnly = true;
            this.destinationTextBox.Size = new System.Drawing.Size(224, 20);
            this.destinationTextBox.TabIndex = 5;
            this.destinationTextBox.TextChanged += new System.EventHandler(this.DestinationTextBox_TextChanged);
            // 
            // destinationButton
            // 
            this.destinationButton.Location = new System.Drawing.Point(305, 170);
            this.destinationButton.Name = "destinationButton";
            this.destinationButton.Size = new System.Drawing.Size(25, 21);
            this.destinationButton.TabIndex = 6;
            this.destinationButton.Text = "...";
            this.destinationButton.UseVisualStyleBackColor = true;
            this.destinationButton.Click += new System.EventHandler(this.DestinationButton_Click);
            // 
            // differentialRadioButton
            // 
            this.differentialRadioButton.AutoSize = true;
            this.differentialRadioButton.Location = new System.Drawing.Point(6, 46);
            this.differentialRadioButton.Name = "differentialRadioButton";
            this.differentialRadioButton.Size = new System.Drawing.Size(75, 17);
            this.differentialRadioButton.TabIndex = 1;
            this.differentialRadioButton.Text = "Differential";
            this.differentialRadioButton.UseVisualStyleBackColor = true;
            this.differentialRadioButton.CheckedChanged += new System.EventHandler(this.DifferentialRadioButton_CheckedChanged);
            // 
            // retentionLabel
            // 
            this.retentionLabel.AutoSize = true;
            this.retentionLabel.Location = new System.Drawing.Point(6, 99);
            this.retentionLabel.Name = "retentionLabel";
            this.retentionLabel.Size = new System.Drawing.Size(56, 13);
            this.retentionLabel.TabIndex = 3;
            this.retentionLabel.Text = "Retention:";
            // 
            // retentionNumericUpDown
            // 
            this.retentionNumericUpDown.Enabled = false;
            this.retentionNumericUpDown.Location = new System.Drawing.Point(95, 97);
            this.retentionNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.retentionNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.retentionNumericUpDown.Name = "retentionNumericUpDown";
            this.retentionNumericUpDown.Size = new System.Drawing.Size(65, 20);
            this.retentionNumericUpDown.TabIndex = 4;
            this.retentionNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // directoriesGroupBox
            // 
            this.directoriesGroupBox.Controls.Add(this.sourceLabel);
            this.directoriesGroupBox.Controls.Add(this.destinationLabel);
            this.directoriesGroupBox.Controls.Add(this.sourceTreeView);
            this.directoriesGroupBox.Controls.Add(this.sourceTextBox);
            this.directoriesGroupBox.Controls.Add(this.destinationButton);
            this.directoriesGroupBox.Controls.Add(this.sourceButton);
            this.directoriesGroupBox.Controls.Add(this.destinationTextBox);
            this.directoriesGroupBox.Location = new System.Drawing.Point(12, 38);
            this.directoriesGroupBox.Name = "directoriesGroupBox";
            this.directoriesGroupBox.Size = new System.Drawing.Size(339, 200);
            this.directoriesGroupBox.TabIndex = 2;
            this.directoriesGroupBox.TabStop = false;
            this.directoriesGroupBox.Text = "Directories";
            // 
            // methodGroupBox
            // 
            this.methodGroupBox.Controls.Add(this.methodHelpButton);
            this.methodGroupBox.Controls.Add(this.retentionLabel);
            this.methodGroupBox.Controls.Add(this.differentialRadioButton);
            this.methodGroupBox.Controls.Add(this.fullRadioButton);
            this.methodGroupBox.Controls.Add(this.retentionNumericUpDown);
            this.methodGroupBox.Controls.Add(this.incrementalRadioButton);
            this.methodGroupBox.Location = new System.Drawing.Point(12, 324);
            this.methodGroupBox.Name = "methodGroupBox";
            this.methodGroupBox.Size = new System.Drawing.Size(166, 125);
            this.methodGroupBox.TabIndex = 4;
            this.methodGroupBox.TabStop = false;
            this.methodGroupBox.Text = "Method";
            // 
            // methodHelpButton
            // 
            this.methodHelpButton.Image = global::RoboBackup.Properties.Resources.HelpIcon;
            this.methodHelpButton.Location = new System.Drawing.Point(136, 13);
            this.methodHelpButton.Name = "methodHelpButton";
            this.methodHelpButton.Size = new System.Drawing.Size(24, 24);
            this.methodHelpButton.TabIndex = 5;
            this.methodHelpButton.UseVisualStyleBackColor = true;
            this.methodHelpButton.Click += new System.EventHandler(this.MethodHelpButton_Click);
            // 
            // saveTaskButton
            // 
            this.saveTaskButton.Location = new System.Drawing.Point(12, 455);
            this.saveTaskButton.Name = "saveTaskButton";
            this.saveTaskButton.Size = new System.Drawing.Size(339, 23);
            this.saveTaskButton.TabIndex = 6;
            this.saveTaskButton.Text = "SaveTask";
            this.saveTaskButton.Click += new System.EventHandler(this.SaveTaskButton_Click);
            // 
            // scheduleGroupBox
            // 
            this.scheduleGroupBox.Controls.Add(this.timeDateTimePicker);
            this.scheduleGroupBox.Controls.Add(this.timeLabel);
            this.scheduleGroupBox.Controls.Add(this.monthdayNumericUpDown);
            this.scheduleGroupBox.Controls.Add(this.dailyRadioButton);
            this.scheduleGroupBox.Controls.Add(this.weeklyRadioButton);
            this.scheduleGroupBox.Controls.Add(this.weekdayComboBox);
            this.scheduleGroupBox.Controls.Add(this.monthlyRadioButton);
            this.scheduleGroupBox.Location = new System.Drawing.Point(185, 324);
            this.scheduleGroupBox.Name = "scheduleGroupBox";
            this.scheduleGroupBox.Size = new System.Drawing.Size(166, 125);
            this.scheduleGroupBox.TabIndex = 5;
            this.scheduleGroupBox.TabStop = false;
            this.scheduleGroupBox.Text = "Schedule";
            // 
            // timeDateTimePicker
            // 
            this.timeDateTimePicker.CustomFormat = "HH:mm";
            this.timeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeDateTimePicker.Location = new System.Drawing.Point(106, 97);
            this.timeDateTimePicker.Name = "timeDateTimePicker";
            this.timeDateTimePicker.ShowUpDown = true;
            this.timeDateTimePicker.Size = new System.Drawing.Size(51, 20);
            this.timeDateTimePicker.TabIndex = 6;
            this.timeDateTimePicker.Value = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(6, 100);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(43, 13);
            this.timeLabel.TabIndex = 5;
            this.timeLabel.Text = "AtTime:";
            // 
            // monthdayNumericUpDown
            // 
            this.monthdayNumericUpDown.Location = new System.Drawing.Point(106, 72);
            this.monthdayNumericUpDown.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.monthdayNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.monthdayNumericUpDown.Name = "monthdayNumericUpDown";
            this.monthdayNumericUpDown.Size = new System.Drawing.Size(51, 20);
            this.monthdayNumericUpDown.TabIndex = 4;
            this.monthdayNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.monthdayNumericUpDown.ValueChanged += new System.EventHandler(this.MonthdayNumericUpDown_ValueChanged);
            // 
            // dailyRadioButton
            // 
            this.dailyRadioButton.AutoSize = true;
            this.dailyRadioButton.Checked = true;
            this.dailyRadioButton.Location = new System.Drawing.Point(6, 20);
            this.dailyRadioButton.Name = "dailyRadioButton";
            this.dailyRadioButton.Size = new System.Drawing.Size(48, 17);
            this.dailyRadioButton.TabIndex = 0;
            this.dailyRadioButton.TabStop = true;
            this.dailyRadioButton.Text = "Daily";
            this.dailyRadioButton.UseVisualStyleBackColor = true;
            // 
            // weeklyRadioButton
            // 
            this.weeklyRadioButton.AutoSize = true;
            this.weeklyRadioButton.Location = new System.Drawing.Point(6, 46);
            this.weeklyRadioButton.Name = "weeklyRadioButton";
            this.weeklyRadioButton.Size = new System.Drawing.Size(87, 17);
            this.weeklyRadioButton.TabIndex = 1;
            this.weeklyRadioButton.Text = "DayOfWeek:";
            this.weeklyRadioButton.UseVisualStyleBackColor = true;
            // 
            // weekdayComboBox
            // 
            this.weekdayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weekdayComboBox.Location = new System.Drawing.Point(106, 45);
            this.weekdayComboBox.Name = "weekdayComboBox";
            this.weekdayComboBox.Size = new System.Drawing.Size(51, 21);
            this.weekdayComboBox.TabIndex = 2;
            this.weekdayComboBox.SelectedIndexChanged += new System.EventHandler(this.WeekdayComboBox_SelectedIndexChanged);
            // 
            // monthlyRadioButton
            // 
            this.monthlyRadioButton.AutoSize = true;
            this.monthlyRadioButton.Location = new System.Drawing.Point(6, 72);
            this.monthlyRadioButton.Name = "monthlyRadioButton";
            this.monthlyRadioButton.Size = new System.Drawing.Size(88, 17);
            this.monthlyRadioButton.TabIndex = 3;
            this.monthlyRadioButton.Text = "DayOfMonth:";
            this.monthlyRadioButton.UseVisualStyleBackColor = true;
            // 
            // networkCredentialsGroupBox
            // 
            this.networkCredentialsGroupBox.Controls.Add(this.passwordTextBox);
            this.networkCredentialsGroupBox.Controls.Add(this.usernameTextBox);
            this.networkCredentialsGroupBox.Controls.Add(this.passwordLabel);
            this.networkCredentialsGroupBox.Controls.Add(this.usernameLabel);
            this.networkCredentialsGroupBox.Location = new System.Drawing.Point(12, 244);
            this.networkCredentialsGroupBox.Name = "networkCredentialsGroupBox";
            this.networkCredentialsGroupBox.Size = new System.Drawing.Size(339, 74);
            this.networkCredentialsGroupBox.TabIndex = 3;
            this.networkCredentialsGroupBox.TabStop = false;
            this.networkCredentialsGroupBox.Text = "NetworkCredentials";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Enabled = false;
            this.passwordTextBox.Location = new System.Drawing.Point(75, 45);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(255, 20);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Enabled = false;
            this.usernameTextBox.Location = new System.Drawing.Point(75, 19);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(255, 20);
            this.usernameTextBox.TabIndex = 1;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(6, 48);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "Password:";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(6, 22);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username:";
            // 
            // sourceFolderBrowserDialog
            // 
            this.sourceFolderBrowserDialog.SelectedPath = null;
            // 
            // destinationFolderBrowserDialog
            // 
            this.destinationFolderBrowserDialog.SelectedPath = null;
            // 
            // TaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 485);
            this.Controls.Add(this.networkCredentialsGroupBox);
            this.Controls.Add(this.scheduleGroupBox);
            this.Controls.Add(this.saveTaskButton);
            this.Controls.Add(this.methodGroupBox);
            this.Controls.Add(this.directoriesGroupBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.titleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Task";
            ((System.ComponentModel.ISupportInitialize)(this.retentionNumericUpDown)).EndInit();
            this.directoriesGroupBox.ResumeLayout(false);
            this.directoriesGroupBox.PerformLayout();
            this.methodGroupBox.ResumeLayout(false);
            this.methodGroupBox.PerformLayout();
            this.scheduleGroupBox.ResumeLayout(false);
            this.scheduleGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthdayNumericUpDown)).EndInit();
            this.networkCredentialsGroupBox.ResumeLayout(false);
            this.networkCredentialsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.RadioButton fullRadioButton;
        private System.Windows.Forms.RadioButton incrementalRadioButton;
        private System.Windows.Forms.TreeView sourceTreeView;
        private System.Windows.Forms.TextBox sourceTextBox;
        private System.Windows.Forms.Button sourceButton;
        private System.Windows.Forms.TextBox destinationTextBox;
        private System.Windows.Forms.Button destinationButton;
        private System.Windows.Forms.RadioButton differentialRadioButton;
        private System.Windows.Forms.Label retentionLabel;
        private System.Windows.Forms.NumericUpDown retentionNumericUpDown;
        private System.Windows.Forms.GroupBox directoriesGroupBox;
        private System.Windows.Forms.GroupBox methodGroupBox;
        private System.Windows.Forms.Button saveTaskButton;
        private System.Windows.Forms.GroupBox scheduleGroupBox;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.NumericUpDown monthdayNumericUpDown;
        private System.Windows.Forms.ComboBox weekdayComboBox;
        private System.Windows.Forms.RadioButton monthlyRadioButton;
        private System.Windows.Forms.RadioButton weeklyRadioButton;
        private System.Windows.Forms.RadioButton dailyRadioButton;
        private System.Windows.Forms.DateTimePicker timeDateTimePicker;
        private System.Windows.Forms.Button methodHelpButton;
        private OpenFolderDialog sourceFolderBrowserDialog;
        private OpenFolderDialog destinationFolderBrowserDialog;
        private System.Windows.Forms.GroupBox networkCredentialsGroupBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label usernameLabel;
    }
}
