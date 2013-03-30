// ***********************************************************************
// Assembly         : VfSpeaker
// Author           : Cameleer
// Created          : 12-16-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-17-2012
// ***********************************************************************
// <copyright file="ProgressForm.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>
    /// Class ProgressForm
    /// </summary>
    public partial class ProgressForm : RadForm, IProgressCallback
    {
        /// <summary>
        /// Delegate SetCommentInvoker
        /// </summary>
        /// <param name="text">The text.</param>
        public delegate void SetCommentInvoker(String text);
        /// <summary>
        /// Delegate IncrementInvoker
        /// </summary>
        /// <param name="val">The val.</param>
        public delegate void IncrementInvoker(int val);
        /// <summary>
        /// Delegate StepToInvoker
        /// </summary>
        /// <param name="val">The val.</param>
        public delegate void StepToInvoker(int val);
        /// <summary>
        /// Delegate RangeInvoker
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        public delegate void RangeInvoker(int minimum, int maximum);

        /// <summary>
        /// The init event
        /// </summary>
        private System.Threading.ManualResetEvent initEvent = new System.Threading.ManualResetEvent(false);
        /// <summary>
        /// The abort event
        /// </summary>
        private System.Threading.ManualResetEvent abortEvent = new System.Threading.ManualResetEvent(false);
        /// <summary>
        /// The requires close
        /// </summary>
        private bool requiresClose = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressForm" /> class.
        /// </summary>
        public ProgressForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Call this method from the worker thread to initialize
        /// the progress callback.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Begin(int minimum, int maximum)
        {
            initEvent.WaitOne();
            Invoke(new RangeInvoker(DoBegin), new object[] { minimum, maximum });
        }

        /// <summary>
        /// Call this method from the worker thread to initialize
        /// the progress callback, without setting the range
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Begin()
        {
            initEvent.WaitOne();
            Invoke(new MethodInvoker(DoBegin));
        }

        /// <summary>
        /// Call this method from the worker thread to reset the range in the
        /// progress callback
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetRange(int minimum, int maximum)
        {
            initEvent.WaitOne();
            Invoke(new RangeInvoker(DoSetRange), new object[] { minimum, maximum });
        }

        /// <summary>
        /// Call this method from the worker thread to update the progress text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetComment(string text)
        {
            if (IsAborting)
            {
                return;
            }

            Invoke(new SetCommentInvoker(DoSetComment), new object[] { text });
        }

        /// <summary>
        /// Call this method from the worker thread to increase the progress
        /// counter by a specified value.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void StepTo(int val)
        {
            Invoke(new StepToInvoker(DoStepTo), new object[] { val });
        }

        /// <summary>
        /// Call this method from the worker thread to step the progress meter to a
        /// particular value.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Increment(int val)
        {
            if (IsAborting)
            {
                return;
            }

            Invoke(new IncrementInvoker(DoIncrement), new object[] { val });
        }

        /// <summary>
        /// If this property is true, then you should abort work
        /// </summary>
        /// <value><c>true</c> if this instance is aborting; otherwise, <c>false</c>.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsAborting
        {
            get
            {
                return abortEvent.WaitOne(0, false);
            }
        }

        /// <summary>
        /// Call this method from the worker thread to finalize the progress meter
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void End()
        {
            if (requiresClose)
            {
                Invoke(new MethodInvoker(DoEnd));
            }
        }

        /// <summary>
        /// Does the begin.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        private void DoBegin(int minimum, int maximum)
        {
            DoBegin();
            DoSetRange(minimum, maximum);
        }

        /// <summary>
        /// Does the begin.
        /// </summary>
        private void DoBegin()
        {
            btnCancel.Enabled = true;
            ControlBox = true;
        }

        /// <summary>
        /// Does the set comment.
        /// </summary>
        /// <param name="text">The text.</param>
        private void DoSetComment(String text)
        {
            tbComment.Text = text;
        }

        /// <summary>
        /// Does the set range.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        private void DoSetRange(int minimum, int maximum)
        {
            progressBar.Minimum = minimum;
            progressBar.Maximum = maximum;
            progressBar.Value1 = minimum;
        }

        /// <summary>
        /// Does the increment.
        /// </summary>
        /// <param name="val">The val.</param>
        private void DoIncrement(int val)
        {
            progressBar.Value1 += val;
            UpdateStatusText();
        }

        private void DoStepTo(int val)
        {
            progressBar.Value1 = val;
            UpdateStatusText();
        }

        private void DoEnd()
        {
            Close();
        }

        /// <summary>
        /// Utility function to terminate the thread
        /// </summary>
        private void AbortWork()
        {
            abortEvent.Set();
        }

        /// <summary>
        /// Utility function that formats and updates the title bar text
        /// </summary>
        private void UpdateStatusText()
        {
            lblProgress.Text = String.Format("{0}% complete", (progressBar.Value1 * 100) / (progressBar.Maximum - progressBar.Minimum));
        }

        /// <summary>
        /// In this override we reset the RootElement's BackColor property
        /// since the DocumentDesigner class sets the BackColor of the
        /// Form to Control when initializing and thus overrides the theme.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            ControlBox = false;
            initEvent.Set();
        }

        /// <summary>
        /// Handler for 'Close' clicking
        /// </summary>
        /// <param name="e">Объект <see cref="T:System.ComponentModel.CancelEventArgs" />, содержащий данные, которые относятся к событию.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            requiresClose = false;
            AbortWork();
            base.OnClosing(e);
        }

    }
}
