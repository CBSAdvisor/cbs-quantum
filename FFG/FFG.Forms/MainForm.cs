using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FFG.Core;
using System.Drawing.Drawing2D;
using FFG.Controls.PopupControl;
using FFG.Configuration;
using System.Configuration;

namespace FFG.Forms
{
    public enum BetMode
    { 
        Wait,
        Coast,
        Attack
    }

    public partial class MainForm : Form
    {
        private RTable _rTable = null;
        Popup _selNumberPopup = null;
        RoulettePopup _rTblPopup = null;

        ///////////////////////////////////////////////////////////////////////
        // Private members for betting
        private double _balance = 27;
        private BetMode _betMode = BetMode.Wait;
        private double _unitCost = 0.01;
        private double _betSumInWaitMode = 0.01;
        private int[] _numberOfUnitsToBet9G = new int[11] { 1, 1, 2, 2, 3, 4, 6, 8, 12, 16, 22 };
        private int _betLevel9G = -1;
        private RNumberGroup _activeNGroup = null;
        ///////////////////////////////////////////////////////////////////////

        private Label[] _DStatValueLabels;

        private string[] _autoNumbers = null;
        private bool _useAutoNumbers = false;
        private int _autoNumbersIndex = 0;

        public MainForm()
        {
            InitializeComponent();

            _DStatValueLabels = new Label[3] { 
                _lbl1DStatValue,
                _lbl2DStatValue,
                _lbl3DStatValue,
            };

            _chHistory.AutoResize(ColumnHeaderAutoResizeStyle.None);
            _chHistory.Width = _lvHistory.ClientSize.Width;
            _chHistory.TextAlign = HorizontalAlignment.Center;

            _rTblPopup = new RoulettePopup();
            _rTblPopup.RButtonClick += new RButtonClickEventHandler(_rTblPopup_RButtonClick);
            _selNumberPopup = new Popup(_rTblPopup);
            _selNumberPopup.Resizable = true;

            _rTable = new RTable();

            _lblColorValue.Text = String.Empty;
            _lblColorValue.ForeColor = System.Drawing.Color.White;
            _lblParityValue.Text = String.Empty;
            _lblParityValue.ForeColor = System.Drawing.Color.White;
            _lblMoreLessValue.Text = String.Empty;
            _lblMoreLessValue.ForeColor = System.Drawing.Color.White;
            _lblDozenValue.Text = String.Empty;
            _lblDozenValue.ForeColor = System.Drawing.Color.White;
            _lblColumnValue.Text = String.Empty;
            _lblColumnValue.ForeColor = System.Drawing.Color.White;

            _cbNumber.SelectedIndex = 0;
            RefreshAllStats();

            FfgConfigSection ffgSection = (FfgConfigSection)ConfigurationManager.GetSection(FfgConfigSection.sConfigurationSectionConst);
            _useAutoNumbers = ffgSection.UseAutoNumbers;
            _balance = ffgSection.Balance;
            _unitCost = ffgSection.UnitCost;

            _autoNumbers = Utils.GetRandomNumbers("rand-500.txt");

            SetBets();
        }

        private void RefreshBetInfo()
        {
            _lblBalance.Text = String.Format("Balance: {0:##0.00}", _balance);
            _lblNumbersForBet.Text = (_activeNGroup != null) ? _activeNGroup.ToString() : String.Empty;
            _lblUnitPerBet.Text = String.Format("{0:##0.00}", (double)((_betMode == BetMode.Coast) ? _numberOfUnitsToBet9G[_betLevel9G] * _unitCost : _betSumInWaitMode));
            _lblSumBets.Text = String.Format("{0:##0.00}", CalcSumBets());
        }

        private RNumberGroup Find9G_WithNineConsecutiveMisses()
        { 
            RNumberGroup resultGroup = null;
            List<RNumberGroup[]> list9Groups = new List<RNumberGroup[]>() { 
                _rTable.Group9_n1, 
                //_rTable.Group9_n2, 
                //_rTable.Group9_n3, 
                //_rTable.Group9_n4 
            };

            list9Groups.ForEach((nineGroup) => {
                foreach (RNumberGroup num9GPart in nineGroup)
                {
                    double p = num9GPart.CalcP(_rTable.Spin);
                    if (p <= 91)
                    {
                        continue;
                    }

                    if (resultGroup == null || (resultGroup.CalcP(_rTable.Spin) < p))
                    {
                        resultGroup = num9GPart;
                        continue;
                    }
                }
            });

            return resultGroup;
        }

        private RNumberGroup FindSixLine_WithMaxConsecutiveMisses()
        {
            RNumberGroup resultGroup = null;
            List<RNumberGroup> list9Groups = new List<RNumberGroup>() { 
                _rTable.SixLine1, 
                _rTable.SixLine2, 
                _rTable.SixLine3, 
                _rTable.SixLine4, 
                _rTable.SixLine5, 
                _rTable.SixLine6, 
                _rTable.SixLine7, 
                _rTable.SixLine8, 
                _rTable.SixLine9, 
                _rTable.SixLine10, 
                _rTable.SixLine11, 
            };

            list9Groups.ForEach((sixLine) =>
            {
                double p = sixLine.CalcP(_rTable.Spin);
                if (resultGroup == null || (resultGroup.CalcP(_rTable.Spin) < p))
                {
                    resultGroup = sixLine;
                }
            });

            return resultGroup;
        }

        private double CalcSumBets()
        {
            switch (_betMode)
            {
                case BetMode.Wait:
                    return _betSumInWaitMode;
                case BetMode.Coast:
                    return 9 * _numberOfUnitsToBet9G[_betLevel9G] * _unitCost;
                case BetMode.Attack:
                    return 0;
                default:
                    return 0;
            }
        }

        private void SetBets()
        {
            switch (_betMode)
            { 
                case BetMode.Wait:
                    _activeNGroup = Find9G_WithNineConsecutiveMisses();
                    if (_activeNGroup != null)
                    {
                        _betMode = BetMode.Coast;
                        _betLevel9G = 0;

                        _balance -= CalcSumBets();
                    }
                    else 
                    {
                        _activeNGroup = FindSixLine_WithMaxConsecutiveMisses();
                        _balance -= CalcSumBets();
                        _betLevel9G = -1;
                    }
                    break;
                case BetMode.Coast:
                    if (_activeNGroup != null && _activeNGroup.Count == 9)
                    {
                        _betMode = BetMode.Coast;
                        _betLevel9G++;
                        _balance -= CalcSumBets();
                    }
                    break;
                case BetMode.Attack:
                    break;
                default:
                    return;
            }
            RefreshBetInfo();
        }

        private void CalcBetResult(RStatNumber rNum)
        {
            switch (_betMode)
            {
                case BetMode.Wait:
                    if (_activeNGroup.FindByNumber(rNum.Number) != null)
                    {
                        _balance += _betSumInWaitMode * 6;
                    }
                    break;
                case BetMode.Coast:
                    if (_activeNGroup.FindByNumber(rNum.Number) != null)
                    {
                        _balance += _numberOfUnitsToBet9G[_betLevel9G] * _unitCost * 36;
                        _betLevel9G = -1;
                        _betMode = BetMode.Wait;
                    }
                    else if (_betLevel9G == 10)
                    {
                        _betLevel9G = -1;
                        _betMode = BetMode.Wait;
                    }
                    break;
                case BetMode.Attack:
                    break;
                default:
                    return;
            }

            RefreshBetInfo();
        }

        private void _btnSpin_Click(object sender, EventArgs e)
        {
            //Random rnd = new Random((int)DateTime.Now.Ticks);
            RStatNumber n = null;

            if (_useAutoNumbers)
            {
                n = new RStatNumber(Int32.Parse(_autoNumbers[_autoNumbersIndex]));
                _autoNumbersIndex++;
                if (_autoNumbersIndex == _autoNumbers.Length)
                {
                    _useAutoNumbers = false;
                }
            }
            else
            {
                n = new RStatNumber(Int32.Parse(_cbNumber.SelectedItem.ToString()));
            }

            _rTable.AddSpin(n.Number);
            ListViewItem lvItem = new ListViewItem(n.Number.ToString());
            n.Spin = _rTable.Spin;
            lvItem.Tag = n;
            _lvHistory.Items.Add(lvItem);
            _lvHistory.Items[_lvHistory.Items.Count - 1].EnsureVisible();

            RefreshSpinLabelInfo(n);
            RefreshAllStats();

            CalcBetResult(n);
            SetBets();
        }

        private void RefreshAllStats()
        {
            _lblSpinStatValue.Text = _rTable.Spin.ToString();

            RefreshDozenStatInfo();
            RefreshColumnStatInfo();
            RefreshColorStatInfo();
            RefreshParityStatInfo();
            RefreshMoreLessStatInfo();
            RefreshStreetStatInfo();
            RefreshSixLineStatInfo();

            _ctrl9G1.RefreshStatistic(_rTable.Group9_n1, _rTable.Spin);
            _ctrl9G2.RefreshStatistic(_rTable.Group9_n2, _rTable.Spin);
            _ctrl9G3.RefreshStatistic(_rTable.Group9_n3, _rTable.Spin);
            _ctrl9G4.RefreshStatistic(_rTable.Group9_n4, _rTable.Spin);
        }

        private void RefreshSpinLabelInfo(RNumber n)
        {
            _lblColorValue.Text = n.Number.ToString();

            switch (n.Color)
            {
                case Core.Color.Red:
                    _lblColorValue.BackColor = System.Drawing.Color.Red;
                    _lblParityValue.BackColor = System.Drawing.Color.Red;
                    _lblMoreLessValue.BackColor = System.Drawing.Color.Red;
                    _lblDozenValue.BackColor = System.Drawing.Color.Red;
                    _lblColumnValue.BackColor = System.Drawing.Color.Red;
                    break;
                case Core.Color.Black:
                    _lblColorValue.BackColor = System.Drawing.Color.Black;
                    _lblParityValue.BackColor = System.Drawing.Color.Black;
                    _lblMoreLessValue.BackColor = System.Drawing.Color.Black;
                    _lblDozenValue.BackColor = System.Drawing.Color.Black;
                    _lblColumnValue.BackColor = System.Drawing.Color.Black;
                    break;
                default:
                    _lblColorValue.BackColor = System.Drawing.Color.Green;
                    _lblParityValue.BackColor = System.Drawing.Color.Green;
                    _lblMoreLessValue.BackColor = System.Drawing.Color.Green;
                    _lblDozenValue.BackColor = System.Drawing.Color.Green;
                    _lblColumnValue.BackColor = System.Drawing.Color.Green;
                    break;
            }

            _lblParityValue.Text = Enum.GetName(typeof(Parity), n.Parity);
            _lblMoreLessValue.Text = Enum.GetName(typeof(MoreLess), n.MoreLess);
            _lblDozenValue.Text = Enum.GetName(typeof(Dozen), n.Dozen);
            _lblColumnValue.Text = Enum.GetName(typeof(Column), n.Column);
        }

        private void RefreshDozenStatInfo()
        {
            RefreshDozenStatInfo(1);
            RefreshDozenStatInfo(2);
            RefreshDozenStatInfo(3);
        }

        private void RefreshDozenStatInfo(int dozen)
        {
            double p = _rTable.GetDozenP(dozen);
            Utils.SetLabelStyle(_DStatValueLabels[dozen - 1], p);
        }

        private void RefreshColumnStatInfo()
        {
            _lbl1CStatValue.Text = _rTable.GetColumnP(1).ToString("0.00");
            _lbl2CStatValue.Text = _rTable.GetColumnP(2).ToString("0.00");
            _lbl3CStatValue.Text = _rTable.GetColumnP(3).ToString("0.00");
        }

        private void RefreshColorStatInfo()
        {
            _lblRedStatValue.Text = _rTable.GetColorP(FFG.Core.Color.Red).ToString("0.00");
            _lblBlackStatValue.Text = _rTable.GetColorP(FFG.Core.Color.Black).ToString("0.00");
        }

        private void RefreshParityStatInfo()
        {
            _lblOddStatValue.Text = _rTable.GetParityP(Parity.Odd).ToString("0.00");
            _lblEvenStatValue.Text = _rTable.GetParityP(Parity.Even).ToString("0.00");
        }

        private void RefreshMoreLessStatInfo()
        {
            _lblMoreStatValue.Text = _rTable.GetMoreLessP(MoreLess.More).ToString("0.00");
            _lblLessStatValue.Text = _rTable.GetMoreLessP(MoreLess.Less).ToString("0.00");
        }

        private void RefreshStreetStatInfo()
        {
            _lbl1StStatValue.Text = _rTable.GetStreetP(Street.One).ToString("0.00");
            _lbl2StStatValue.Text = _rTable.GetStreetP(Street.Two).ToString("0.00");
            _lbl3StStatValue.Text = _rTable.GetStreetP(Street.Three).ToString("0.00");
            _lbl4StStatValue.Text = _rTable.GetStreetP(Street.Four).ToString("0.00");
            _lbl5StStatValue.Text = _rTable.GetStreetP(Street.Five).ToString("0.00");
            _lbl6StStatValue.Text = _rTable.GetStreetP(Street.Six).ToString("0.00");
            _lbl7StStatValue.Text = _rTable.GetStreetP(Street.Seven).ToString("0.00");
            _lbl8StStatValue.Text = _rTable.GetStreetP(Street.Eight).ToString("0.00");
            _lbl9StStatValue.Text = _rTable.GetStreetP(Street.Nine).ToString("0.00");
            _lbl10StStatValue.Text = _rTable.GetStreetP(Street.Ten).ToString("0.00");
            _lbl11StStatValue.Text = _rTable.GetStreetP(Street.Eleven).ToString("0.00");
            _lbl12StStatValue.Text = _rTable.GetStreetP(Street.Twelve).ToString("0.00");
        }

        private void RefreshSixLineStatInfo()
        {
            _lbl1SlStatValue.Text = _rTable.GetSixLineP(SixLine.One).ToString("0.00");
            _lbl2SlStatValue.Text = _rTable.GetSixLineP(SixLine.Two).ToString("0.00");
            _lbl3SlStatValue.Text = _rTable.GetSixLineP(SixLine.Three).ToString("0.00");
            _lbl4SlStatValue.Text = _rTable.GetSixLineP(SixLine.Four).ToString("0.00");
            _lbl5SlStatValue.Text = _rTable.GetSixLineP(SixLine.Five).ToString("0.00");
            _lbl6SlStatValue.Text = _rTable.GetSixLineP(SixLine.Six).ToString("0.00");
            _lbl7SlStatValue.Text = _rTable.GetSixLineP(SixLine.Seven).ToString("0.00");
            _lbl8SlStatValue.Text = _rTable.GetSixLineP(SixLine.Eight).ToString("0.00");
            _lbl9SlStatValue.Text = _rTable.GetSixLineP(SixLine.Nine).ToString("0.00");
            _lbl10SlStatValue.Text = _rTable.GetSixLineP(SixLine.Ten).ToString("0.00");
            _lbl11SlStatValue.Text = _rTable.GetSixLineP(SixLine.Eleven).ToString("0.00");
        }

        private void _lvHistory_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            RStatNumber number = (RStatNumber)e.Item.Tag;
            Brush brush;

            switch (number.Color)
            { 
                case Core.Color.Red:
                    e.Item.BackColor = System.Drawing.Color.Red;
                    brush = Brushes.Red;
                    break;
                case Core.Color.Black:
                    e.Item.BackColor = System.Drawing.Color.Black;
                    brush = Brushes.Black;
                    break;
                default:
                    e.Item.BackColor = System.Drawing.Color.Green;
                    brush = Brushes.Green;
                    break;
            }
            e.Graphics.FillRectangle(brush, e.Bounds);
            e.DrawText(TextFormatFlags.HorizontalCenter);
            if ((e.State & ListViewItemStates.Selected) != 0)
            {
                e.DrawFocusRectangle();
            } 
        }

        private void _lvHistory_ClientSizeChanged(object sender, EventArgs e)
        {
            _chHistory.Width = ((ListView)sender).ClientSize.Width;
        }

        private void _btnSelNumber_Click(object sender, EventArgs e)
        {
            if (!_selNumberPopup.Visible)
            {
                _selNumberPopup.Show(sender as Button);
            }
            else 
            {
                _selNumberPopup.Hide();
            }
        }

        private void _rTblPopup_RButtonClick(object sender, RButtonClickEventArgs e)
        {
            _lblNextNumber.Text = e.Number.ToString();
            _selNumberPopup.Hide();
        }
    }
}
