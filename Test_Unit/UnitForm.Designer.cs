namespace Test_Unit
{
    partial class UnitForm
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
            this.uiReportTextBox = new System.Windows.Forms.TextBox();
            this.uiSendReportButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.uiReceivedAlertsTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // uiReportTextBox
            // 
            this.uiReportTextBox.Location = new System.Drawing.Point(13, 13);
            this.uiReportTextBox.Multiline = true;
            this.uiReportTextBox.Name = "uiReportTextBox";
            this.uiReportTextBox.Size = new System.Drawing.Size(259, 74);
            this.uiReportTextBox.TabIndex = 0;
            // 
            // uiSendReportButton
            // 
            this.uiSendReportButton.Location = new System.Drawing.Point(176, 94);
            this.uiSendReportButton.Name = "uiSendReportButton";
            this.uiSendReportButton.Size = new System.Drawing.Size(96, 23);
            this.uiSendReportButton.TabIndex = 1;
            this.uiSendReportButton.Text = "Send Report";
            this.uiSendReportButton.UseVisualStyleBackColor = true;
            this.uiSendReportButton.Click += new System.EventHandler(this.uiSendReportButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Received Alerts:";
            // 
            // uiReceivedAlertsTextBox
            // 
            this.uiReceivedAlertsTextBox.Location = new System.Drawing.Point(13, 147);
            this.uiReceivedAlertsTextBox.Multiline = true;
            this.uiReceivedAlertsTextBox.Name = "uiReceivedAlertsTextBox";
            this.uiReceivedAlertsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uiReceivedAlertsTextBox.Size = new System.Drawing.Size(259, 74);
            this.uiReceivedAlertsTextBox.TabIndex = 3;
            // 
            // UnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 235);
            this.Controls.Add(this.uiReceivedAlertsTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiSendReportButton);
            this.Controls.Add(this.uiReportTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "UnitForm";
            this.Text = "Unit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uiReportTextBox;
        private System.Windows.Forms.Button uiSendReportButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uiReceivedAlertsTextBox;
    }
}

