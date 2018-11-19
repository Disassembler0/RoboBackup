namespace RoboBackup
{
    partial class MainForm
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newTaskToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editTaskToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteTaskToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.aboutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showTaskLogsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.runTaskToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.taskListView = new System.Windows.Forms.ListView();
            this.titleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.scheduleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sourceColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.destinationColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTaskToolStripButton,
            this.toolStripSeparator1,
            this.editTaskToolStripButton,
            this.deleteTaskToolStripButton,
            this.aboutToolStripButton,
            this.toolStripSeparator2,
            this.showTaskLogsToolStripButton,
            this.runTaskToolStripButton,
            this.settingsToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(726, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // newTaskToolStripButton
            // 
            this.newTaskToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newTaskToolStripButton.Image = global::RoboBackup.Properties.Resources.NewIcon;
            this.newTaskToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newTaskToolStripButton.Name = "newTaskToolStripButton";
            this.newTaskToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newTaskToolStripButton.Text = "NewTask";
            this.newTaskToolStripButton.Click += new System.EventHandler(this.NewTaskToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // editTaskToolStripButton
            // 
            this.editTaskToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editTaskToolStripButton.Enabled = false;
            this.editTaskToolStripButton.Image = global::RoboBackup.Properties.Resources.EditIcon;
            this.editTaskToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editTaskToolStripButton.Name = "editTaskToolStripButton";
            this.editTaskToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.editTaskToolStripButton.Text = "EditTask";
            this.editTaskToolStripButton.Click += new System.EventHandler(this.EditTaskToolStripButton_Click);
            // 
            // deleteTaskToolStripButton
            // 
            this.deleteTaskToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteTaskToolStripButton.Enabled = false;
            this.deleteTaskToolStripButton.Image = global::RoboBackup.Properties.Resources.DeleteIcon;
            this.deleteTaskToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteTaskToolStripButton.Name = "deleteTaskToolStripButton";
            this.deleteTaskToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deleteTaskToolStripButton.Text = "DeleteTask";
            this.deleteTaskToolStripButton.Click += new System.EventHandler(this.DeleteTaskToolStripButton_Click);
            // 
            // aboutToolStripButton
            // 
            this.aboutToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aboutToolStripButton.Image = global::RoboBackup.Properties.Resources.HelpIcon;
            this.aboutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutToolStripButton.Name = "aboutToolStripButton";
            this.aboutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.aboutToolStripButton.Text = "About";
            this.aboutToolStripButton.Click += new System.EventHandler(this.AboutToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // showTaskLogsToolStripButton
            // 
            this.showTaskLogsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showTaskLogsToolStripButton.Enabled = false;
            this.showTaskLogsToolStripButton.Image = global::RoboBackup.Properties.Resources.LogsIcon;
            this.showTaskLogsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showTaskLogsToolStripButton.Name = "showTaskLogsToolStripButton";
            this.showTaskLogsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.showTaskLogsToolStripButton.Text = "ShowTaskLogs";
            this.showTaskLogsToolStripButton.Click += new System.EventHandler(this.ShowTaskLogsToolStripButton_Click);
            // 
            // runTaskToolStripButton
            // 
            this.runTaskToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.runTaskToolStripButton.Enabled = false;
            this.runTaskToolStripButton.Image = global::RoboBackup.Properties.Resources.RunIcon;
            this.runTaskToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runTaskToolStripButton.Name = "runTaskToolStripButton";
            this.runTaskToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.runTaskToolStripButton.Text = "RunTask";
            this.runTaskToolStripButton.Click += new System.EventHandler(this.RunTaskToolStripButton_Click);
            // 
            // settingsToolStripButton
            // 
            this.settingsToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.settingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsToolStripButton.Image = global::RoboBackup.Properties.Resources.SettingsIcon;
            this.settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStripButton.Name = "settingsToolStripButton";
            this.settingsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.settingsToolStripButton.Text = "Settings";
            this.settingsToolStripButton.Click += new System.EventHandler(this.SettingsToolStripButton_Click);
            // 
            // taskListView
            // 
            this.taskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColumnHeader,
            this.scheduleColumnHeader,
            this.sourceColumnHeader,
            this.destinationColumnHeader});
            this.taskListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskListView.FullRowSelect = true;
            this.taskListView.GridLines = true;
            this.taskListView.Location = new System.Drawing.Point(0, 25);
            this.taskListView.Name = "taskListView";
            this.taskListView.Size = new System.Drawing.Size(726, 289);
            this.taskListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.taskListView.TabIndex = 2;
            this.taskListView.UseCompatibleStateImageBehavior = false;
            this.taskListView.View = System.Windows.Forms.View.Details;
            this.taskListView.SelectedIndexChanged += new System.EventHandler(this.TaskListView_SelectedIndexChanged);
            this.taskListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TaskListView_MouseDoubleClick);
            // 
            // titleColumnHeader
            // 
            this.titleColumnHeader.Text = "Title";
            this.titleColumnHeader.Width = 160;
            // 
            // scheduleColumnHeader
            // 
            this.scheduleColumnHeader.Text = "Schedule";
            this.scheduleColumnHeader.Width = 160;
            // 
            // sourceColumnHeader
            // 
            this.sourceColumnHeader.Text = "Source";
            this.sourceColumnHeader.Width = 200;
            // 
            // destinationColumnHeader
            // 
            this.destinationColumnHeader.Text = "Destination";
            this.destinationColumnHeader.Width = 200;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 314);
            this.Controls.Add(this.taskListView);
            this.Controls.Add(this.toolStrip);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RoboBackup";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton newTaskToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton editTaskToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteTaskToolStripButton;
        private System.Windows.Forms.ToolStripButton aboutToolStripButton;
        private System.Windows.Forms.ListView taskListView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton showTaskLogsToolStripButton;
        private System.Windows.Forms.ToolStripButton runTaskToolStripButton;
        private System.Windows.Forms.ColumnHeader titleColumnHeader;
        private System.Windows.Forms.ColumnHeader scheduleColumnHeader;
        private System.Windows.Forms.ColumnHeader sourceColumnHeader;
        private System.Windows.Forms.ColumnHeader destinationColumnHeader;
        private System.Windows.Forms.ToolStripButton settingsToolStripButton;
    }
}
