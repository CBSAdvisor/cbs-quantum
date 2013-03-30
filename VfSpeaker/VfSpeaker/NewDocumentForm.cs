using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace VfSpeaker
{
    public partial class NewDocumentForm : RadForm
    {
        public NewDocumentForm(string title, string directory)
        {
            InitializeComponent();

            Title = title;
            Directory = directory;
        }

        public string Title { get { return tbName.Text; } set { tbName.Text = value; } }

        public string Directory { get { return tbDirectory.Value; } set { tbDirectory.Value = value; } }
    }
}
