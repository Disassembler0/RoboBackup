namespace RoboBackup {
    partial class AboutForm {
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
            this.aboutNameLabel = new System.Windows.Forms.Label();
            this.aboutVersionLabel = new System.Windows.Forms.Label();
            this.aboutAuthorLabel = new System.Windows.Forms.Label();
            this.aboutSourceCodeLabel = new System.Windows.Forms.Label();
            this.sourceCodeLinkLabel = new System.Windows.Forms.LinkLabel();
            this.authorLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // aboutNameLabel
            // 
            this.aboutNameLabel.AutoSize = true;
            this.aboutNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.aboutNameLabel.ForeColor = System.Drawing.Color.Green;
            this.aboutNameLabel.Location = new System.Drawing.Point(12, 9);
            this.aboutNameLabel.Name = "aboutNameLabel";
            this.aboutNameLabel.Size = new System.Drawing.Size(210, 37);
            this.aboutNameLabel.TabIndex = 0;
            this.aboutNameLabel.Text = "RoboBackup";
            // 
            // aboutVersionLabel
            // 
            this.aboutVersionLabel.AutoSize = true;
            this.aboutVersionLabel.Location = new System.Drawing.Point(16, 56);
            this.aboutVersionLabel.Name = "aboutVersionLabel";
            this.aboutVersionLabel.Size = new System.Drawing.Size(45, 13);
            this.aboutVersionLabel.TabIndex = 1;
            this.aboutVersionLabel.Text = "Version:";
            // 
            // aboutAuthorLabel
            // 
            this.aboutAuthorLabel.AutoSize = true;
            this.aboutAuthorLabel.Location = new System.Drawing.Point(16, 82);
            this.aboutAuthorLabel.Name = "aboutAuthorLabel";
            this.aboutAuthorLabel.Size = new System.Drawing.Size(41, 13);
            this.aboutAuthorLabel.TabIndex = 3;
            this.aboutAuthorLabel.Text = "Author:";
            // 
            // aboutSourceCodeLabel
            // 
            this.aboutSourceCodeLabel.AutoSize = true;
            this.aboutSourceCodeLabel.Location = new System.Drawing.Point(16, 108);
            this.aboutSourceCodeLabel.Name = "aboutSourceCodeLabel";
            this.aboutSourceCodeLabel.Size = new System.Drawing.Size(69, 13);
            this.aboutSourceCodeLabel.TabIndex = 5;
            this.aboutSourceCodeLabel.Text = "SourceCode:";
            // 
            // sourceCodeLinkLabel
            // 
            this.sourceCodeLinkLabel.AutoSize = true;
            this.sourceCodeLinkLabel.Location = new System.Drawing.Point(93, 108);
            this.sourceCodeLinkLabel.Name = "sourceCodeLinkLabel";
            this.sourceCodeLinkLabel.Size = new System.Drawing.Size(236, 13);
            this.sourceCodeLinkLabel.TabIndex = 6;
            this.sourceCodeLinkLabel.TabStop = true;
            this.sourceCodeLinkLabel.Text = "https://github.com/Disassembler0/RoboBackup";
            this.sourceCodeLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SourceCodeLinkLabel_LinkClicked);
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(93, 82);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(194, 13);
            this.authorLabel.TabIndex = 4;
            this.authorLabel.Text = "Disassembler <disassembler@dasm.cz>";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(93, 56);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(40, 13);
            this.versionLabel.TabIndex = 2;
            this.versionLabel.Text = "0.0.0.0";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 138);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.sourceCodeLinkLabel);
            this.Controls.Add(this.aboutSourceCodeLabel);
            this.Controls.Add(this.aboutAuthorLabel);
            this.Controls.Add(this.aboutVersionLabel);
            this.Controls.Add(this.aboutNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label aboutNameLabel;
        private System.Windows.Forms.Label aboutVersionLabel;
        private System.Windows.Forms.Label aboutAuthorLabel;
        private System.Windows.Forms.Label aboutSourceCodeLabel;
        private System.Windows.Forms.LinkLabel sourceCodeLinkLabel;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Label versionLabel;
    }
}
