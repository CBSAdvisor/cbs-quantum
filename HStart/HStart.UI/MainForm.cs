using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HStart.UI
{
    public partial class MainForm : Form
    {
        private ProcessStartInfo _minerdProcInfo;
        private Process _minerdProcess;

        private NotifyIcon _trayIcon;
        private ContextMenu _trayMenu;

        private MenuItem _miRunStop;
        private MenuItem _miExit;

        public MainForm()
        {
            InitializeComponent();

            // Create a simple tray menu with only one item.
            _trayMenu = new ContextMenu();

            _miRunStop = new MenuItem("Run", OnRunStopMenuItem);
            _miRunStop.Visible = true;
            _trayMenu.MenuItems.Add(_miRunStop);

            _miExit = new MenuItem("Exit", OnApplicationExit);
            _miExit.Visible = true;
            _trayMenu.MenuItems.Add(_miExit);

            // Create a tray icon.
            _trayIcon = new NotifyIcon();
            _trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            _trayIcon.BalloonTipTitle = "Nidden Proccess Starter";
            _trayIcon.BalloonTipText = "Minerd was started.";
            _trayIcon.Text = "Nidden minerd started.";
            _trayIcon.Icon = (Icon)this.Icon.Clone();

            // Add menu to tray icon and show it.
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _minerdProcInfo = CreateProccessInfo();

            _trayIcon.ShowBalloonTip(5000, "HProc Starter", "Hidden Proccess starter running.", ToolTipIcon.Info);
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartProcess));
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

        private void StartProcess(object param)
        {
            if (_minerdProcess == null || _minerdProcess.HasExited)
            {
                _minerdProcess = new Process();
                _minerdProcess.EnableRaisingEvents = true;
                _minerdProcess.StartInfo = _minerdProcInfo;
                _minerdProcess.Exited += _minerdProcess_Exited;
                _minerdProcess.OutputDataReceived += new DataReceivedEventHandler(_minerdProcess_OutputDataReceived);

                _minerdProcess.Start();
                _minerdProcess.BeginOutputReadLine();

                OnProccessStarted(_minerdProcess, new EventArgs());
            }

        }

        private void StopProcess()
        {
            if (_minerdProcess != null && !_minerdProcess.HasExited)
            {
                _minerdProcess.Kill();
            }
        }

        private void _minerdProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke(new MethodInvoker(
                delegate
                {
                }));
        }

        private void _minerdProcess_Exited(object sender, EventArgs e)
        {
            OnProccessStoped(sender, e);
        }

        private void OnProccessStarted(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(
                delegate
                {
                    _trayIcon.ShowBalloonTip(5000, "HProc Starter", "Proccess minerd started.", ToolTipIcon.Info);
                    _miRunStop.Text = "Stop";
                }));
        }

        private void OnProccessStoped(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(
                delegate
                {
                    _minerdProcess = null;
                    _trayIcon.ShowBalloonTip(5000, "HProc Starter", "Proccess minerd stoped.", ToolTipIcon.Info);
                    _miRunStop.Text = "Run";
                }));
        }

        private void OnRunStopMenuItem(object sender, EventArgs e)
        {
            if (_minerdProcess == null || _minerdProcess.HasExited)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(StartProcess));
            }
            else 
            {
                StopProcess();
            }
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
