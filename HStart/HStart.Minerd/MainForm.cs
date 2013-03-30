using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HStart.Minerd
{
    public partial class MainForm : Form
    {
        private ProcessStartInfo _minerdProcInfo;
        private Process _minerdProcess;

        public MainForm()
        {
            InitializeComponent();

            //Environment.CurrentDirectory
        }

        private ProcessStartInfo CreateProccessInfo()
        {
            ProcessStartInfo procInfo = new ProcessStartInfo();

            procInfo.CreateNoWindow = true;
            procInfo.UseShellExecute = false;
            procInfo.RedirectStandardOutput = true;
            procInfo.RedirectStandardError = true;
            procInfo.FileName = "minerd.exe";
            procInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //procInfo.Arguments = @"-q --userpass=Cameleer.3:helicopter --proxy=192.168.21.1:3128 --url=http://coinotron.com:8322 --algo=scrypt --threads=8 --scantime=6 --retry-pause=10";
            procInfo.Arguments = @"-q --userpass=Cameleer.1:helicopter --url=http://coinotron.com:8322 --algo=scrypt --threads=2 --scantime=6 --retry-pause=10";

            return procInfo;
        }

        private void _btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (_minerdProcess == null)
                {
                    _minerdProcess = new Process();
                    _minerdProcess.EnableRaisingEvents = true;
                    _minerdProcess.StartInfo = _minerdProcInfo;
                    _minerdProcess.Exited += _minerdProcess_Exited;
                    _minerdProcess.OutputDataReceived += new DataReceivedEventHandler(_minerdProcess_OutputDataReceived);
                    
                    _btnRun.Text = "Stop";
                    _lblProcOutput.Text = string.Empty;

                    _minerdProcess.Start();
                    _minerdProcess.BeginOutputReadLine();
                }
                else
                {
                    _minerdProcess.Kill();
                }
            }
            catch
            {
                // Log error.
            }
        }

        private void _minerdProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke(new MethodInvoker(
                delegate
                {
                    _lblProcOutput.Text = e.Data;
                }));
        }

        private void _minerdProcess_Exited(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(
                delegate
                {
                    _minerdProcess = null;
                    _btnRun.Text = "Run";
                }));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _minerdProcInfo = CreateProccessInfo();
        }
    }
}
