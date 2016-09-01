using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace DCodeSerialComm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker bw = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void browse_button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse DCode Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "dcode";
            openFileDialog1.Filter = "DCode files (*.dcode)|*.dcode|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dcode_filename.Text = openFileDialog1.FileName;
            }
        }

        private void verify_send_button_Click(object sender, RoutedEventArgs e)
        {
            if (bw.IsBusy != true)
            {
                bw.WorkerReportsProgress = true;
                bw.WorkerSupportsCancellation = true;
                bw.DoWork += bw_DoWork;
                bw.ProgressChanged += bw_ProgressChanged;
                bw.RunWorkerCompleted += bw_RunWorkerCompleted;
                verify_send_button.IsEnabled = false;
                cancel_button.IsEnabled = true;
                bw.RunWorkerAsync();
            }
            else
            {
                tbProgress.Text = "Error Setting Up Background Worker!";
            }
            
        }


        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 0; (i <= 100); i++)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(50);
                    worker.ReportProgress((i));
                }
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            send_progress_bar.Value = (e.ProgressPercentage);
            progress_bar_percent.Text = (e.ProgressPercentage.ToString() + "%");
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                tbProgress.Text = "Canceled!";
                //verify_send_button.IsEnabled = true;
            }

            else if (!(e.Error == null))
            {
                tbProgress.Text = ("Error: " + e.Error.Message);
                cancel_button.IsEnabled = false;
            }

            else
            {
                tbProgress.Text = "Done!";
                cancel_button.IsEnabled = false;
            }
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            if (bw.CancellationPending != true && bw.WorkerSupportsCancellation == true)
            {
                bw.CancelAsync();
                cancel_button.IsEnabled = false;
            }
        }

        private void find_com_port_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
