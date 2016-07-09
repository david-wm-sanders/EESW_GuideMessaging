namespace Test_CommandCentre
{
    partial class CommandCentreForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.uiReceivedReportsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uiPublishAlertButton = new System.Windows.Forms.Button();
            this.uiAlertTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // uiReceivedReportsTextBox
            // 
            this.uiReceivedReportsTextBox.Location = new System.Drawing.Point(13, 146);
            this.uiReceivedReportsTextBox.Multiline = true;
            this.uiReceivedReportsTextBox.Name = "uiReceivedReportsTextBox";
            this.uiReceivedReportsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uiReceivedReportsTextBox.Size = new System.Drawing.Size(259, 74);
            this.uiReceivedReportsTextBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Received Reports:";
            // 
            // uiPublishAlertButton
            // 
            this.uiPublishAlertButton.Location = new System.Drawing.Point(176, 93);
            this.uiPublishAlertButton.Name = "uiPublishAlertButton";
            this.uiPublishAlertButton.Size = new System.Drawing.Size(96, 23);
            this.uiPublishAlertButton.TabIndex = 5;
            this.uiPublishAlertButton.Text = "Publish Alert";
            this.uiPublishAlertButton.UseVisualStyleBackColor = true;
            this.uiPublishAlertButton.Click += new System.EventHandler(this.uiPublishAlertButton_Click);
            // 
            // uiAlertTextBox
            // 
            this.uiAlertTextBox.Location = new System.Drawing.Point(13, 12);
            this.uiAlertTextBox.Multiline = true;
            this.uiAlertTextBox.Name = "uiAlertTextBox";
            this.uiAlertTextBox.Size = new System.Drawing.Size(259, 74);
            this.uiAlertTextBox.TabIndex = 4;
            // 
            // CommandCentreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 232);
            this.Controls.Add(this.uiReceivedReportsTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiPublishAlertButton);
            this.Controls.Add(this.uiAlertTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CommandCentreForm";
            this.Text = "Command Centre";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uiReceivedReportsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button uiPublishAlertButton;
        private System.Windows.Forms.TextBox uiAlertTextBox;
    }
}

