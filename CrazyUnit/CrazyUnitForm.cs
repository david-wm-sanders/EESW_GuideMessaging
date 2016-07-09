using System;
using System.Windows.Forms;
using GuideMessaging;

namespace CrazyUnit
{
    public partial class CrazyUnitForm : Form
    {
        Unit unit;
        System.Timers.Timer t;

        public CrazyUnitForm()
        {
            InitializeComponent();

            unit = new Unit("Unit.txt");
            t = new System.Timers.Timer(10);
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
        }

        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            unit.Send("TEST");
        }

        private void uiBeginReportBlitzButton_Click(object sender, EventArgs e)
        {
            t.Start();
        }
    }
}
