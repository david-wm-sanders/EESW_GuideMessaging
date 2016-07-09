using System;
using System.Text;
using System.Windows.Forms;
using GuideMessaging;

namespace Test_Unit
{
    public partial class UnitForm : Form
    {
        Unit unit;

        public UnitForm()
        {
            InitializeComponent();
            unit = new Unit("Unit.txt");
            unit.AlertReceived += new MessageReceivedEventHandler(unit_AlertReceived);
        }

        void unit_AlertReceived(object sender, NewMessageEventArgs message)
        {
            StringBuilder sb = new StringBuilder(uiReceivedAlertsTextBox.Text);
            sb.AppendLine(Encoding.Unicode.GetString(message.Data));
            uiReceivedAlertsTextBox.Text = sb.ToString();
        }

        private void uiSendReportButton_Click(object sender, EventArgs e)
        {
            if (uiReportTextBox.TextLength > 0)
            {
                unit.Send(uiReportTextBox.Text);
                uiReportTextBox.Clear();
            }
        }
    }
}
