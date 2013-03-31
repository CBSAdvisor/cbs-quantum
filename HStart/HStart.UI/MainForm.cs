using HStart.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        private ProcessStartInfo _procInfoStarter;
        private Process _processStarter;

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
            _procInfoStarter = CreateProccessInfo();

            _trayIcon.ShowBalloonTip(5000, "HProc Starter", "Hidden Proccess starter running.", ToolTipIcon.Info);
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartProcess));
        }

        private ProcessStartInfo CreateProccessInfo()
        {
            ProcessStartInfo procInfo = new ProcessStartInfo();

            StartProcInfoSection spiSection = (StartProcInfoSection)ConfigurationManager.GetSection(StartProcInfoSection.sConfigurationSectionConst);

            procInfo.CreateNoWindow = spiSection.StartProcInfos[0].CreateNoWindow;
            procInfo.UseShellExecute = spiSection.StartProcInfos[0].UseShellExecute;
            procInfo.RedirectStandardOutput = spiSection.StartProcInfos[0].RedirectStandardOutput;
            procInfo.RedirectStandardError = spiSection.StartProcInfos[0].RedirectStandardError;
            procInfo.FileName = spiSection.StartProcInfos[0].FileName;
            procInfo.WindowStyle = spiSection.StartProcInfos[0].WindowStyle;
            //procInfo.Arguments = @"-q --userpass=Cameleer.3:helicopter --proxy=192.168.21.1:3128 --url=http://coinotron.com:8322 --algo=scrypt --threads=8 --scantime=6 --retry-pause=10";
            procInfo.Arguments = spiSection.StartProcInfos[0].Arguments;

            return procInfo;
        }

        private void StartProcess(object param)
        {
            if (_processStarter == null || _processStarter.HasExited)
            {
                _processStarter = new Process();
                _processStarter.EnableRaisingEvents = true;
                _processStarter.StartInfo = _procInfoStarter;
                _processStarter.Exited += _processStarter_Exited;
                _processStarter.OutputDataReceived += new DataReceivedEventHandler(_processStarter_OutputDataReceived);

                _processStarter.Start();
                _processStarter.BeginOutputReadLine();

                OnProccessStarted(_processStarter, new EventArgs());
            }

        }

        private void StopProcess()
        {
            if (_processStarter != null && !_processStarter.HasExited)
            {
                _processStarter.Kill();
            }
        }

        private void _processStarter_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke(new MethodInvoker(
                delegate
                {
                }));
        }

        private void _processStarter_Exited(object sender, EventArgs e)
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
                    _processStarter = null;
                    _trayIcon.ShowBalloonTip(5000, "HProc Starter", "Proccess minerd stoped.", ToolTipIcon.Info);
                    _miRunStop.Text = "Run";
                }));
        }

        private void OnRunStopMenuItem(object sender, EventArgs e)
        {
            if (_processStarter == null || _processStarter.HasExited)
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
