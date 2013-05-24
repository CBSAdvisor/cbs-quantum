using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FFG.Core;

namespace FFG.Controls
{
    [ToolboxBitmap(typeof(System.Windows.Forms.Panel), "9 Group Stat"), ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"), Description("Displays 9 group statistic.")]
    public partial class NineGroupStatControl : UserControl
    {
        Label[] _lblPartNumbers;
        Label[] _lblPartStatValues;
        Label[] _lblPartStatCounts;

        public NineGroupStatControl()
        {
            InitializeComponent();

            _lblPartNumbers = new Label[] { _lbl9GANumbers, _lbl9GBNumbers, _lbl9GCNumbers, _lbl9GDNumbers };
            _lblPartStatValues = new Label[] { _lbl9GAStatValue, _lbl9GBStatValue, _lbl9GCStatValue, _lbl9GDStatValue };
            _lblPartStatCounts = new Label[] { _lbl9GACount, _lbl9GBCount, _lbl9GCCount, _lbl9GDCount };
        }

        public void RefreshStatistic(RNumberGroup[] nineGroup, int spin)
        {
            for (int i = 0; i < nineGroup.Length; i++)
            {
                double p = nineGroup[i].CalcP(spin);
                _lblPartNumbers[i].Text = nineGroup[i].ToString();
                _lblPartStatCounts[i].Text = String.Format("({0,3:##0})", nineGroup[i].GetCount());
                Utils.SetLabelStyle(_lblPartStatValues[i], p);
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string Title { get { return _lblGroupTitle.Text; } set { _lblGroupTitle.Text = value; } }
    }
}
