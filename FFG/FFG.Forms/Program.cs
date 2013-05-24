using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FFG.Configuration;

namespace FFG.Forms
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

            //FfgConfigSection ffgSection = FfgConfigSection.CreateSection();

            //NumbersGroupElement ngElement = new NumbersGroupElement() { 
            //    Key = "9G-1"
            //};

            //ngElement.Parts = new NumbersGroupPartElementCollection() { 
            //    new NumbersGroupPartElement(){ Key=0, Value="14  34  21   5   8  30  23   6  27" },
            //    new NumbersGroupPartElement(){ Key=1, Value="35  36  31  29   9   4  16   2  25" },
            //    new NumbersGroupPartElement(){ Key=2, Value="24  19  33  10  11  20   3  17  15" },
            //    new NumbersGroupPartElement(){ Key=3, Value="26  13   1  28  32  22   7  12  18" },
            //};

            //if (ffgSection.NineGroups[ngElement.Key] == null)
            //{
            //    ffgSection.NineGroups.Add(ngElement);
            //}

            //ffgSection.Save();


            Application.Run(new MainForm());
        }
    }
}
