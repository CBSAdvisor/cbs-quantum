using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FFG.Core
{
    public class Utils
    {
        public static void SetLabelStyle(Label label, double p)
        {
            System.Drawing.Color foreColor;
            System.Drawing.Color backColor;
            FontStyle fontStyle;

            if (p > 90)
            {
                foreColor = System.Drawing.Color.White;
                backColor = System.Drawing.Color.Green;
                fontStyle = FontStyle.Bold;
            }
            else if ((p > 80) && (p <= 90))
            {
                foreColor = System.Drawing.Color.Black;
                backColor = System.Drawing.Color.YellowGreen;
                fontStyle = FontStyle.Bold;
            }
            else if ((p > 70) && (p <= 80))
            {
                foreColor = System.Drawing.Color.Black;
                backColor = System.Drawing.Color.Gold;
                fontStyle = FontStyle.Bold;
            }
            else
            {
                foreColor = System.Drawing.Color.Black;
                backColor = System.Drawing.Color.White;
                fontStyle = FontStyle.Regular;
            }

            label.Text = p.ToString("0.00");
            label.ForeColor = foreColor;
            label.BackColor = backColor;
            label.Font = new Font(label.Font, fontStyle);
        }

        public static string[] GetRandomNumbers(string fileName)
        {
            string[] result = null;

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] buffer = new byte[fs.Length];
                    br.Read(buffer, 0, buffer.Length);
                    result = Encoding.UTF8.GetString(buffer).Split(new string[]{ " ", "\t", ",", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    br.Close();
                }
                fs.Close();
            }

            return result;
        }
    }
}
