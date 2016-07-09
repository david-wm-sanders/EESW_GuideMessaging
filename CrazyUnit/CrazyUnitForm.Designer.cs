namespace CrazyUnit
{
    partial class CrazyUnitForm
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
            this.uiBeginReportBlitzButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uiBeginReportBlitzButton
            // 
            this.uiBeginReportBlitzButton.Location = new System.Drawing.Point(13, 13);
            this.uiBeginReportBlitzButton.Name = "uiBeginReportBlitzButton";
            this.uiBeginReportBlitzButton.Size = new System.Drawing.Size(259, 237);
            this.uiBeginReportBlitzButton.TabIndex = 0;
            this.uiBeginReportBlitzButton.Text = "Start sending lots of reports very very fast";
            this.uiBeginReportBlitzButton.UseVisualStyleBackColor = true;
            this.uiBeginReportBlitzButton.Click += new System.EventHandler(this.uiBeginReportBlitzButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.uiBeginReportBlitzButton);
            this.Name = "Form1";
            this.Text = "Crazy Unit";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uiBeginReportBlitzButton;
    }
}

