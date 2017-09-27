using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp.C7
{
    public partial class BackgroundWorkerForm : Form
    {
        public BackgroundWorkerForm()
        {
            InitializeComponent();

            // next way
            bWorkerSecond.DoWork += delegate(object sender, DoWorkEventArgs e) {
                int i = 0;
                while (!bWorkerSecond.CancellationPending)
                {
                    bWorkerSecond.ReportProgress(i, null);
                    i += 5;
                    e.Result = i;
                    i = i > 100 ? 0 : i;//i eger 0-dan kicikdirse yeniden 100 verilsin    
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            };

            bWorkerSecond.ProgressChanged += bWorkerSecond_ProgressChanged;

            bWorkerSecond.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorkerSecond_RunWorkerCompleted);
        }

        private void bWorkerSecond_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarSecond.Value = 0;
            labelPercentage2.Text = string.Format("{0} %", progressBarSecond.Value);
        }

        void bWorkerSecond_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarSecond.Value = e.ProgressPercentage;
            labelPercentage2.Text = string.Format("{0} %", e.ProgressPercentage);
        }

        private void btnFirstOn_Click(object sender, EventArgs e)
        {
            if (!bWorkerFirst.IsBusy)
                bWorkerFirst.RunWorkerAsync();
        }

        private void btnFirstOff_Click(object sender, EventArgs e)
        {
            if (bWorkerFirst.IsBusy)
                bWorkerFirst.CancelAsync();
        }

        private void btnSecondOn_Click(object sender, EventArgs e)
        {
            if (!bWorkerSecond.IsBusy)
                bWorkerSecond.RunWorkerAsync();
        }

        private void btnSecondOff_Click(object sender, EventArgs e)
        {
            if (bWorkerSecond.IsBusy)
                bWorkerSecond.CancelAsync();
        }

        private void bWorkerFirst_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 100;
            while (!bWorkerFirst.CancellationPending)
            {
                bWorkerFirst.ReportProgress(i, null);
                i-=10;
                e.Result = i;                
                i = i < 0 ? 100 : i;//i eger 0-dan kicikdirse yeniden 100 verilsin     
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
        }

        private void bWorkerFirst_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarFirst.Value = e.ProgressPercentage;
            labelPercentage1.Text = string.Format("{0} %", e.ProgressPercentage);
        }

        private void bWorkerFirst_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarFirst.Value = 100;
            labelPercentage1.Text = string.Format("{0} %", progressBarFirst.Value);
        }
    }
}
