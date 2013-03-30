namespace VfSpeaker.Views
{
    partial class VfDocumentView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VfDocumentView));
            this.pagesView = new Telerik.WinControls.UI.RadPageView();
            this.DocumentPage = new Telerik.WinControls.UI.RadPageViewPage();
            this.PartsPage = new Telerik.WinControls.UI.RadPageViewPage();
            ((System.ComponentModel.ISupportInitialize)(this.pagesView)).BeginInit();
            this.pagesView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // pagesView
            // 
            this.pagesView.Controls.Add(this.DocumentPage);
            this.pagesView.Controls.Add(this.PartsPage);
            this.pagesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagesView.Location = new System.Drawing.Point(0, 0);
            this.pagesView.Name = "pagesView";
            this.pagesView.SelectedPage = this.DocumentPage;
            this.pagesView.Size = new System.Drawing.Size(254, 203);
            this.pagesView.TabIndex = 0;
            this.pagesView.Text = "PagesView";
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.pagesView.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.None;
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.pagesView.GetChildAt(0))).ItemFitMode = Telerik.WinControls.UI.StripViewItemFitMode.Fill;
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.pagesView.GetChildAt(0))).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top;
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.pagesView.GetChildAt(0))).ItemSpacing = 2;
            // 
            // DocumentPage
            // 
            this.DocumentPage.Location = new System.Drawing.Point(10, 37);
            this.DocumentPage.Name = "DocumentPage";
            this.DocumentPage.Size = new System.Drawing.Size(233, 155);
            this.DocumentPage.Text = "Document";
            this.DocumentPage.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PartsPage
            // 
            this.PartsPage.Location = new System.Drawing.Point(10, 37);
            this.PartsPage.Name = "PartsPage";
            this.PartsPage.Size = new System.Drawing.Size(233, 155);
            this.PartsPage.Text = "Parts";
            this.PartsPage.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VfDocumentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 203);
            this.Controls.Add(this.pagesView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VfDocumentView";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "ControlForm";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.VfDocumentView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pagesView)).EndInit();
            this.pagesView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPageView pagesView;
        public Telerik.WinControls.UI.RadPageViewPage DocumentPage;
        public Telerik.WinControls.UI.RadPageViewPage PartsPage;


    }
}
