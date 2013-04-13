using Cbs.LogLib;
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
using log4net;

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

        private Task _monitorProcess = null;
        private bool _needMonitorProcess = false;
        private CancellationTokenSource _ctsMonitorProcess;

        public MainForm()
        {
            InitializeComponent();

            Application.ApplicationExit += Application_ApplicationExit;

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
            _trayIcon.BalloonTipTitle = "Nidden Process Starter";
            _trayIcon.BalloonTipText = "Minerd was started.";
            _trayIcon.Text = "Nidden minerd started.";
            _trayIcon.Icon = (Icon)this.Icon.Clone();

            // Add menu to tray icon and show it.
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
        }

        public Task MonitorProcessAsync(string processName)
        {
            _ctsMonitorProcess = new CancellationTokenSource();
            CancellationToken ctsMonitorProcess = _ctsMonitorProcess.Token;

            Task task = new Task((pingProcessName) =>
            {
                Log4.UserLog.InfoFormat("Process monitoring started : {0}.", pingProcessName);

                while (true)
                {
                    Thread.Sleep(1500);

                    if (ctsMonitorProcess.IsCancellationRequested)
                    {
                        break;
                    }

                    int count = Process.GetProcessesByName((string)pingProcessName).Count();

                    if (count == 0)
                    {
                        Log4.UserLog.InfoFormat("Process \"{0}\" not found. Monitor will try restart.", pingProcessName);

                        ThreadPool.QueueUserWorkItem(new WaitCallback(StartProcess));
                    }
                }

                Log4.UserLog.InfoFormat("Process monitoring stoped : {0}.", pingProcessName);

            }, processName, ctsMonitorProcess);

            return task;

            //return Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(1500);

            //        if (ctsMonitorProcess.IsCancellationRequested)
            //        {
            //            break;
            //        }

            //        int count = Process.GetProcessesByName(processName).Count();

            //        if (count == 0)
            //        {
            //            //ThreadPool.QueueUserWorkItem(new WaitCallback(StartProcess));
            //        }
            //    }
            //}, ctsMonitorProcess);
        }

        protected override void OnLoad(EventArgs e)
        {
            Log4.UserLog.InfoFormat("\"{0}\" v{1} started.", Application.ProductName, Application.ProductVersion);

            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _procInfoStarter = CreateProcessInfo();

            _trayIcon.ShowBalloonTip(5000, "HProc Starter", "Hidden Process starter running.", ToolTipIcon.Info);

            ThreadPool.QueueUserWorkItem(new WaitCallback(StartProcess));
        }

        private ProcessStartInfo CreateProcessInfo()
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
            procInfo.WorkingDirectory = spiSection.StartProcInfos[0].WorkingDirectory;

            Log4.UserLog.InfoFormat("Created process info for {0}", procInfo.FileName);

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
                //_processStarter.BeginOutputReadLine();

                OnProcessStarted(_processStarter, new EventArgs());
            }

        }

        private void StopProcess()
        {
            if (_processStarter != null && !_processStarter.HasExited)
            {
                if (_monitorProcess != null && _monitorProcess.Status == TaskStatus.Running)
                {
                    _ctsMonitorProcess.Cancel();
                    _monitorProcess.Wait();
                }

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
            OnProcessStoped((Process)sender, e);
        }

        private void OnProcessStarted(object sender, EventArgs e)
        {
            Log4.UserLog.InfoFormat("Process {0} started.", ((Process)sender).ProcessName);

            if (_monitorProcess != null && _monitorProcess.Status == TaskStatus.Running)
            {
                _ctsMonitorProcess.Cancel();
                _monitorProcess.Wait();
            }

            _monitorProcess = MonitorProcessAsync(_processStarter.ProcessName);

            _monitorProcess.Start();

            this.Invoke(new MethodInvoker(
                delegate
                {
                    _trayIcon.ShowBalloonTip(5000, "HProc Starter", "Process minerd started.", ToolTipIcon.Info);
                    _miRunStop.Text = "Stop";
                }));
        }

        private void OnProcessStoped(Process sender, EventArgs e)
        {
            Log4.UserLog.InfoFormat("Process stoped. Exit code {0}", ((Process)sender).ExitCode);
            this.Invoke(new MethodInvoker(
                delegate
                {
                    _processStarter = null;
                    _trayIcon.ShowBalloonTip(5000, "HProc Starter", "Process minerd stoped.", ToolTipIcon.Info);
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

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            StopProcess();
            Log4.UserLog.InfoFormat("\"{0}\" v{1} exited.", Application.ProductName, Application.ProductVersion);
        }
    }
}
