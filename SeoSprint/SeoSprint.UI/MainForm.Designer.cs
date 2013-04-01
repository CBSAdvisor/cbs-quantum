namespace SeoSprint.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            this.progressBar = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
            this.btnStart = new Infragistics.Win.Misc.UltraButton();
            this.btnClose = new Infragistics.Win.Misc.UltraButton();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            appearance9.BackColor = System.Drawing.Color.Transparent;
            appearance9.FontData.BoldAsString = "True";
            appearance9.ForeColor = System.Drawing.Color.White;
            appearance9.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance9.ImageBackground")));
            appearance9.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.progressBar.Appearance = appearance9;
            this.progressBar.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance10.BackColor = System.Drawing.Color.Transparent;
            appearance10.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance10.ImageBackground")));
            appearance10.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.progressBar.FillAppearance = appearance10;
            this.progressBar.Location = new System.Drawing.Point(12, 12);
            this.progressBar.Name = "progressBar";
            this.progressBar.PercentFormat = "P2";
            this.progressBar.Size = new System.Drawing.Size(420, 26);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 1;
            this.progressBar.Text = "[Formatted]";
            this.progressBar.UseAppStyling = false;
            this.progressBar.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.progressBar.Value = 50;
            // 
            // btnStart
            // 
            appearance11.BackColor = System.Drawing.Color.Transparent;
            appearance11.BorderColor = System.Drawing.Color.Transparent;
            appearance11.BorderColor3DBase = System.Drawing.Color.Transparent;
            appearance11.Image = ((object)(resources.GetObject("appearance11.Image")));
            appearance11.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Centered;
            appearance11.ImageHAlign = Infragistics.Win.HAlign.Center;
            appearance11.ImageVAlign = Infragistics.Win.VAlign.Middle;
            this.btnStart.Appearance = appearance11;
            this.btnStart.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            appearance12.BackColor = System.Drawing.Color.Transparent;
            appearance12.BorderColor = System.Drawing.Color.Transparent;
            appearance12.BorderColor3DBase = System.Drawing.Color.Transparent;
            appearance12.Image = ((object)(resources.GetObject("appearance12.Image")));
            appearance12.ImageHAlign = Infragistics.Win.HAlign.Center;
            appearance12.ImageVAlign = Infragistics.Win.VAlign.Middle;
            this.btnStart.HotTrackAppearance = appearance12;
            this.btnStart.ImageSize = new System.Drawing.Size(0, 0);
            this.btnStart.Location = new System.Drawing.Point(438, 12);
            this.btnStart.Name = "btnStart";
            appearance13.BackColor = System.Drawing.Color.Transparent;
            appearance13.BorderColor = System.Drawing.Color.Transparent;
            appearance13.BorderColor3DBase = System.Drawing.Color.Transparent;
            appearance13.Image = ((object)(resources.GetObject("appearance13.Image")));
            appearance13.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Centered;
            appearance13.ImageHAlign = Infragistics.Win.HAlign.Center;
            appearance13.ImageVAlign = Infragistics.Win.VAlign.Middle;
            this.btnStart.PressedAppearance = appearance13;
            this.btnStart.ShowFocusRect = false;
            this.btnStart.ShowOutline = false;
            this.btnStart.Size = new System.Drawing.Size(37, 26);
            this.btnStart.TabIndex = 2;
            this.btnStart.UseAppStyling = false;
            this.btnStart.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClose
            // 
            appearance14.BackColor = System.Drawing.Color.Transparent;
            appearance14.BorderColor = System.Drawing.Color.Transparent;
            appearance14.BorderColor3DBase = System.Drawing.Color.Transparent;
            appearance14.Image = ((object)(resources.GetObject("appearance14.Image")));
            appearance14.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Centered;
            appearance14.ImageHAlign = Infragistics.Win.HAlign.Center;
            appearance14.ImageVAlign = Infragistics.Win.VAlign.Middle;
            this.btnClose.Appearance = appearance14;
            this.btnClose.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            appearance15.BackColor = System.Drawing.Color.Transparent;
            appearance15.BorderColor = System.Drawing.Color.Transparent;
            appearance15.BorderColor3DBase = System.Drawing.Color.Transparent;
            appearance15.Image = ((object)(resources.GetObject("appearance15.Image")));
            appearance15.ImageHAlign = Infragistics.Win.HAlign.Center;
            appearance15.ImageVAlign = Infragistics.Win.VAlign.Middle;
            this.btnClose.HotTrackAppearance = appearance15;
            this.btnClose.ImageSize = new System.Drawing.Size(0, 0);
            this.btnClose.Location = new System.Drawing.Point(481, 12);
            this.btnClose.Name = "btnClose";
            appearance16.BackColor = System.Drawing.Color.Transparent;
            appearance16.BorderColor = System.Drawing.Color.Transparent;
            appearance16.BorderColor3DBase = System.Drawing.Color.Transparent;
            appearance16.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Centered;
            appearance16.ImageHAlign = Infragistics.Win.HAlign.Center;
            appearance16.ImageVAlign = Infragistics.Win.VAlign.Middle;
            this.btnClose.PressedAppearance = appearance16;
            this.btnClose.ShowFocusRect = false;
            this.btnClose.ShowOutline = false;
            this.btnClose.Size = new System.Drawing.Size(26, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.UseAppStyling = false;
            this.btnClose.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(519, 49);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SEO Sprint";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinProgressBar.UltraProgressBar progressBar;
        private Infragistics.Win.Misc.UltraButton btnStart;
        private Infragistics.Win.Misc.UltraButton btnClose;
    }
}

