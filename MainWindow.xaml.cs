using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.IO;

namespace DCodeSerialComm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker bw = new BackgroundWorker();
        StreamReader sr;
        SerialPort currentPort;
        bool portFound;
        long file_size;

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
            FileInfo file = new FileInfo(openFileDialog1.FileName);
            file_size = file.Length / 9;    // 9 bytes per line (roughly 8 + \n), so this gives an estimate of the number of lines in the file
            //Pass the file path and file name to the StreamReader constructor
            sr = new StreamReader(openFileDialog1.FileName);
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
            //Read the first line of text
            String line;
            byte[] buffer = new byte[8];
            line = sr.ReadLine();
            long count = 0;
            while (line != null)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        buffer[i] = Convert.ToByte(line[i]);
                    }
                    currentPort.Write(buffer, 0, 8);    // send the current line
                    line = sr.ReadLine();               // read in the next line
                    
                    worker.ReportProgress( (int)((++count * 100) / file_size));
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
            // close the serial port and file
            currentPort.Close();
            sr.Close();
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
            set_ComPort();
            if (portFound == true)
            {
                com_port_text.Text = "Arduino Found! " + currentPort.ToString();
                verify_send_button.IsEnabled = true;
                find_com_port_button.IsEnabled = false;
            }
            else
            {
                com_port_text.Text = "Arduino Not Found!";
            }
        }

        private void set_ComPort()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    currentPort = new SerialPort(port, 38400);
                    if (DetectArduino())
                    {
                        portFound = true;
                        break;
                    }
                    else
                    {
                        portFound = false;
        
            }
                }
            }
            catch (Exception e)
            {
            }
        }
        private bool DetectArduino()
        {
            try
            {
                //The below setting are for the Hello handshake
                byte[] buffer = new byte[5];
                buffer[0] = Convert.ToByte('I');
                buffer[1] = Convert.ToByte('S');
                buffer[2] = Convert.ToByte('A');
                buffer[3] = Convert.ToByte('R');
                buffer[4] = Convert.ToByte('D');
                int intReturnASCII = 0;
                char charReturnValue = (Char)intReturnASCII;
                currentPort.Open();
                currentPort.Write(buffer, 0, 5);
                Thread.Sleep(100);
                int count = currentPort.BytesToRead;
                string returnMessage = "";
                while (count > 0)
                {
                    intReturnASCII = currentPort.ReadByte();
                    returnMessage = returnMessage + Convert.ToChar(intReturnASCII);
                    count--;
                }

                if (returnMessage.Contains("ARD_MEGA"))
                {
                    return true;
                }
                else
                {
                    currentPort.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
