namespace VfSpeaker.Views
{
    partial class VfPartsListView
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
            Telerik.WinControls.UI.ListViewDetailColumn listViewDetailColumn1 = new Telerik.WinControls.UI.ListViewDetailColumn("Column 0", "Text");
            this.lvParts = new Telerik.WinControls.UI.RadListView();
            ((System.ComponentModel.ISupportInitialize)(this.lvParts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lvParts
            // 
            this.lvParts.AllowArbitraryItemHeight = true;
            this.lvParts.AllowArbitraryItemWidth = true;
            listViewDetailColumn1.HeaderText = "Text";
            this.lvParts.Columns.AddRange(new Telerik.WinControls.UI.ListViewDetailColumn[] {
            listViewDetailColumn1});
            this.lvParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvParts.ItemSize = new System.Drawing.Size(200, 40);
            this.lvParts.ItemSpacing = 2;
            this.lvParts.Location = new System.Drawing.Point(0, 0);
            this.lvParts.Name = "lvParts";
            this.lvParts.Size = new System.Drawing.Size(663, 317);
            this.lvParts.TabIndex = 0;
            this.lvParts.Text = "radListView1";
            this.lvParts.ViewType = Telerik.WinControls.UI.ListViewType.DetailsView;
            this.lvParts.VisualItemFormatting += new Telerik.WinControls.UI.ListViewVisualItemEventHandler(this.lvParts_VisualItemFormatting);
            this.lvParts.CellFormatting += new Telerik.WinControls.UI.ListViewCellFormattingEventHandler(this.lvParts_CellFormatting);
            this.lvParts.CellCreating += new Telerik.WinControls.UI.ListViewCellElementCreatingEventHandler(this.lvParts_CellCreating);
            this.lvParts.Resize += new System.EventHandler(this.lvParts_Resize);
            ((Telerik.WinControls.UI.DetailListViewElement)(this.lvParts.GetChildAt(0).GetChildAt(0))).AllowArbitraryItemHeight = true;
            ((Telerik.WinControls.UI.DetailListViewElement)(this.lvParts.GetChildAt(0).GetChildAt(0))).AllowArbitraryItemWidth = true;
            ((Telerik.WinControls.UI.DetailListViewElement)(this.lvParts.GetChildAt(0).GetChildAt(0))).ItemSize = new System.Drawing.Size(200, 40);
            // 
            // VfPartsListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 317);
            this.Controls.Add(this.lvParts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VfPartsListView";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "VfPartsList";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.VfPartsListView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvParts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadListView lvParts;
    }
}
