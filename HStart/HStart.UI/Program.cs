using HStart.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HStart.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartProcInfoSection spiSection = StartProcInfoSection.CreateSection();

            StartProcInfoElement spiElement = new StartProcInfoElement() { 
                Key = "minerd-coinotron",
                CreateNoWindow = false,
                FileName = "minerd.exe",
                Arguments = @"-q --userpass=Cameleer.1:helicopter --url=http://coinotron.com:8322 --algo=scrypt --threads=2 --scantime=6 --retry-pause=10",
                WindowStyle = ProcessWindowStyle.Maximized
            };

            if (spiSection.StartProcInfos[spiElement.Key] == null)
            {
                spiSection.StartProcInfos.Add(spiElement);
            }

            spiSection.Save();

            Application.Run(new MainForm());
        }
    }
}
