// ***********************************************************************
// Assembly         : VfSpeaker
// Author           : Cameleer
// Created          : 12-08-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-08-2012
// ***********************************************************************
// <copyright file="MainForm.cs" company="">
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
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using VfSpeaker.Data.Repositories;
using VfSpeaker.Views;

namespace VfSpeaker
{
    /// <summary>
    /// Class MainForm
    /// </summary>
    public partial class MainForm : Telerik.WinControls.UI.RadForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            //Office2010BlackTheme officeTheme = new Office2010BlackTheme();
            //officeTheme.Load();
            //ThemeResolutionService.ApplicationThemeName = officeTheme.ThemeName;

            DesertTheme desertTheme = new DesertTheme();
            desertTheme.Load();
            ThemeResolutionService.ApplicationThemeName = desertTheme.ThemeName;

            ThemeResolutionService.ApplyThemeToControlTree(this, ThemeResolutionService.ApplicationThemeName);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        public void DisplayView(RadForm view)
        {
            this.Cursor = Cursors.WaitCursor;
            this.SuspendLayout();
            this.pnlMain.SuspendLayout();

            view.TopLevel = false;

            view.Width = pnlMain.Width;
            view.Height = pnlMain.Height;
            if (pnlMain.Controls.Count == 1)
            {
                Control control = pnlMain.Controls[0];
                control.Visible = false;
                pnlMain.Controls.Clear();
                control.Dispose();
                control = null;
            }
            pnlMain.Controls.Add(view);
            view.Dock = DockStyle.Fill;
            view.Show();
            view.Focus();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Cursor = Cursors.Default;
        }

        public void RefreshView()
        {
            Invoke( new MethodInvoker(((VfDocumentView)(pnlMain.Controls[0])).RefreshView));
        }

        private void miNew_Click(object sender, EventArgs e)
        {
            VfApplication.CurrentContext.NewDocument();
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            ofd.InitialDirectory = VfRepository.DefaultDataDirectory;

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                VfApplication.CurrentContext.OpenDocument(ofd.FileName);
            }
        }

    }
}
