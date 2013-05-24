using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FFG.Controls.ButtonControl;

namespace FFG.Controls.PopupControl
{
    [Serializable]
    [ComVisible(true)]
    public delegate void RButtonClickEventHandler(object sender, RButtonClickEventArgs e);

    [ToolboxBitmap(typeof(System.Windows.Forms.Panel)), ToolboxItem(true),
    ToolboxItemFilter("System.Windows.Forms"), Description("Displays roulette numbers.")]
    public partial class RoulettePopup : UserControl
    {
        public RoulettePopup()
        {
            InitializeComponent();
        }

        protected void OnRButtonClick(RButtonClickEventArgs e)
        {
            if (RButtonClick != null)
            {
                RButtonClick(this, e);
            }
        }

        private void _rBtn_Click(object sender, EventArgs e)
        {
            ImageLblButton btn = sender as ImageLblButton;
            int n = Int32.Parse(btn.Tag.ToString());
            OnRButtonClick(new RButtonClickEventArgs(n));
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        [Description("Ocurs when the number is clicked.")]
        [Browsable(true)]
        public event RButtonClickEventHandler RButtonClick;
    }

    [ComVisible(true)]
    public class RButtonClickEventArgs : EventArgs
    {
        public RButtonClickEventArgs(int n)
        {
            Number = n;
        }

        public int Number { get; set; }
    }
}
