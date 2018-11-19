namespace RoboBackup {
    partial class SettingsForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.languageLabel = new System.Windows.Forms.Label();
            this.logRetentionLabel = new System.Windows.Forms.Label();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.logRetentionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.saveSettingsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logRetentionNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // languageLabel
            // 
            this.languageLabel.AutoSize = true;
            this.languageLabel.Location = new System.Drawing.Point(12, 15);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(58, 13);
            this.languageLabel.TabIndex = 0;
            this.languageLabel.Text = "Language:";
            // 
            // logRetentionLabel
            // 
            this.logRetentionLabel.AutoSize = true;
            this.logRetentionLabel.Location = new System.Drawing.Point(12, 41);
            this.logRetentionLabel.Name = "logRetentionLabel";
            this.logRetentionLabel.Size = new System.Drawing.Size(74, 13);
            this.logRetentionLabel.TabIndex = 2;
            this.logRetentionLabel.Text = "LogRetention:";
            // 
            // languageComboBox
            // 
            this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Location = new System.Drawing.Point(76, 12);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(116, 21);
            this.languageComboBox.TabIndex = 1;
            // 
            // logRetentionNumericUpDown
            // 
            this.logRetentionNumericUpDown.Location = new System.Drawing.Point(121, 39);
            this.logRetentionNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.logRetentionNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.logRetentionNumericUpDown.Name = "logRetentionNumericUpDown";
            this.logRetentionNumericUpDown.Size = new System.Drawing.Size(71, 20);
            this.logRetentionNumericUpDown.TabIndex = 3;
            this.logRetentionNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // saveSettingsButton
            // 
            this.saveSettingsButton.Location = new System.Drawing.Point(15, 65);
            this.saveSettingsButton.Name = "saveSettingsButton";
            this.saveSettingsButton.Size = new System.Drawing.Size(177, 23);
            this.saveSettingsButton.TabIndex = 4;
            this.saveSettingsButton.Text = "SaveSettings";
            this.saveSettingsButton.UseVisualStyleBackColor = true;
            this.saveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 96);
            this.Controls.Add(this.saveSettingsButton);
            this.Controls.Add(this.logRetentionNumericUpDown);
            this.Controls.Add(this.languageComboBox);
            this.Controls.Add(this.logRetentionLabel);
            this.Controls.Add(this.languageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.logRetentionNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.Label logRetentionLabel;
        private System.Windows.Forms.ComboBox languageComboBox;
        private System.Windows.Forms.NumericUpDown logRetentionNumericUpDown;
        private System.Windows.Forms.Button saveSettingsButton;
    }
}
