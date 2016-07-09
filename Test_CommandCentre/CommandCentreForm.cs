using System;
using System.Text;
using System.Windows.Forms;
using GuideMessaging;

namespace Test_CommandCentre
{
    public partial class CommandCentreForm : Form
    {
        CommandCentre commandCentre;

        public CommandCentreForm()
        {
            InitializeComponent();
            commandCentre = new CommandCentre("CommandCentre.txt");
            commandCentre.ReportReceived += new MessageReceivedEventHandler(commandCentre_ReportReceived);
        }

        void commandCentre_ReportReceived(object sender, NewMessageEventArgs message)
        {
            StringBuilder sb = new StringBuilder(uiReceivedReportsTextBox.Text);
            sb.AppendLine(Encoding.Unicode.GetString(message.Data));
            uiReceivedReportsTextBox.Text = sb.ToString();
        }

        private void uiPublishAlertButton_Click(object sender, EventArgs e)
        {
            if (uiAlertTextBox.TextLength > 0) 
            {
                commandCentre.Send(uiAlertTextBox.Text);
                uiAlertTextBox.Clear();
            }
        }
    }
}
