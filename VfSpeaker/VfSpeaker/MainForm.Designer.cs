namespace VfSpeaker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            this.miNew = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.miOpen = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.pnlMain = new Telerik.WinControls.UI.RadPanel();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radMenu1
            // 
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.miNew,
            this.miOpen});
            this.radMenu1.Location = new System.Drawing.Point(0, 0);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(755, 42);
            this.radMenu1.TabIndex = 0;
            this.radMenu1.Text = "radMenu1";
            // 
            // miNew
            // 
            this.miNew.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.miNew.Image = ((System.Drawing.Image)(resources.GetObject("miNew.Image")));
            this.miNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.miNew.Name = "miNew";
            this.miNew.Text = "";
            this.miNew.ToolTipText = "Create new document.";
            this.miNew.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.miNew.Click += new System.EventHandler(this.miNew_Click);
            // 
            // miOpen
            // 
            this.miOpen.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.miOpen.Image = ((System.Drawing.Image)(resources.GetObject("miOpen.Image")));
            this.miOpen.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.miOpen.Name = "miOpen";
            this.miOpen.Text = "";
            this.miOpen.ToolTipText = "Open existing document.";
            this.miOpen.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 42);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(755, 333);
            this.pnlMain.TabIndex = 1;
            // 
            // ofd
            // 
            this.ofd.FileName = "Open document";
            this.ofd.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 375);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.radMenu1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(763, 409);
            this.Name = "MainForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VF Speaker";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadMenu radMenu1;
        private Telerik.WinControls.UI.RadMenuButtonItem miNew;
        private Telerik.WinControls.UI.RadMenuButtonItem miOpen;
        private Telerik.WinControls.UI.RadPanel pnlMain;
        private System.Windows.Forms.OpenFileDialog ofd;

    }
}
