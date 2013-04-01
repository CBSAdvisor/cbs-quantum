// ***********************************************************************
// Assembly         : SeoSprint.UI
// Created          : 02-11-2013
//
// Last Modified On : 02-12-2013
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeoSprint.UI.Extentions;

namespace SeoSprint.UI
{
    /// <summary>
    /// Class MainForm
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The _min value
        /// </summary>
        private int _minValue = 0;
        /// <summary>
        /// The _cur value
        /// </summary>
        private int _curValue = 0;
        /// <summary>
        /// The _period value
        /// </summary>
        private int _periodValue = 10;

        /// <summary>
        /// The _is drag
        /// </summary>
        private bool _isDrag = false;
        /// <summary>
        /// The _start point
        /// </summary>
        private Point _startPoint = new Point(0, 0);

        /// <summary>
        /// The _timer
        /// </summary>
        private System.Threading.Timer _timer = null;
        /// <summary>
        /// The _timer callback handler
        /// </summary>
        private System.Threading.TimerCallback _timerCallbackHandler = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            InitState();

            _timerCallbackHandler = new System.Threading.TimerCallback(TimerCallback);
            _timer = new System.Threading.Timer(_timerCallbackHandler, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        /// <value>The max value.</value>
        public int MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the cur value.
        /// </summary>
        /// <value>The cur value.</value>
        public int CurValue
        {
            get
            {
                return _curValue;
            }
            set
            {
                _curValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the exclude list.
        /// </summary>
        /// <value>The exclude list.</value>
        public string ExcludeList { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MainForm" /> is draggable.
        /// </summary>
        /// <value><c>true</c> if draggable; otherwise, <c>false</c>.</value>
        public bool Draggable { get; set; }

        /// <summary>
        /// Inits the progress bar.
        /// </summary>
        private void InitState()
        {
            MaxValue = 15 * 1000;
            CurValue = 0;

            progressBar.Minimum = _minValue;
            progressBar.Maximum = MaxValue;
            progressBar.Value = 0;
        }

        /// <summary>
        /// Timers the callback.
        /// </summary>
        /// <param name="state">The state.</param>
        private void TimerCallback(object state)
        {
            CurValue += _periodValue;
            if (CurValue <= MaxValue)
            {
                this.Invoke(() => { progressBar.Value = CurValue; });
            }
            else
            {
                _timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            }
        }

        #region Event handlers

        /// <summary>
        /// Handles the Click event of the btnStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            InitState();
            _timer.Change(0, _periodValue);
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            _timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            Close();
        }

        /// <summary>
        /// Handles the MouseDown event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _startPoint = new Point(e.X, e.Y);
                _isDrag = true;
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrag)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this._startPoint.X, p2.Y - this._startPoint.Y);
                this.Location = p3;
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            // Changes the _isDrag field so that the form does
            // not move unless the user is pressing the left mouse button.
            if (e.Button == MouseButtons.Left)
            {
                _isDrag = false;
            }
        }

        #endregion
    }
}
