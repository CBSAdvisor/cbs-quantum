namespace VfSpeaker.Views
{
    partial class VfDocumentEditView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VfDocumentEditView));
            this.tbName = new Telerik.WinControls.UI.RadTextBox();
            this.lblName = new Telerik.WinControls.UI.RadLabel();
            this.lblAuthor = new Telerik.WinControls.UI.RadLabel();
            this.tbAuthor = new Telerik.WinControls.UI.RadTextBox();
            this.lblYear = new Telerik.WinControls.UI.RadLabel();
            this.tbYear = new Telerik.WinControls.UI.RadTextBox();
            this.lblGroup = new Telerik.WinControls.UI.RadLabel();
            this.tbGroup = new Telerik.WinControls.UI.RadTextBox();
            this.lblGenre = new Telerik.WinControls.UI.RadLabel();
            this.tbGenre = new Telerik.WinControls.UI.RadTextBox();
            this.lblComments = new Telerik.WinControls.UI.RadLabel();
            this.tbComments = new Telerik.WinControls.UI.RadTextBox();
            this.tbAudioDirectory = new Telerik.WinControls.UI.RadBrowseEditor();
            this.lblAudioDirectory = new Telerik.WinControls.UI.RadLabel();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnAddFromFile = new Telerik.WinControls.UI.RadButton();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.pnlCover = new Telerik.WinControls.UI.RadPanel();
            this.btnImportCover = new Telerik.WinControls.UI.RadButton();
            this.btnTTS = new Telerik.WinControls.UI.RadButton();
            this.lblFileIndex = new Telerik.WinControls.UI.RadLabel();
            this.tbFileIndex = new Telerik.WinControls.UI.RadTextBox();
            this.iVfDocumentModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tbName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuthor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAuthor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGenre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGenre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblComments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbComments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAudioDirectory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAudioDirectory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddFromFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImportCover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTTS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFileIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFileIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iVfDocumentModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(294, 21);
            this.tbName.Name = "tbName";
            this.tbName.NullText = "<Book Title>";
            this.tbName.Size = new System.Drawing.Size(405, 20);
            this.tbName.TabIndex = 0;
            this.tbName.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(203, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(36, 18);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(203, 49);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(41, 18);
            this.lblAuthor.TabIndex = 3;
            this.lblAuthor.Text = "Author";
            // 
            // tbAuthor
            // 
            this.tbAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAuthor.Location = new System.Drawing.Point(294, 47);
            this.tbAuthor.Name = "tbAuthor";
            this.tbAuthor.NullText = "<Author>";
            this.tbAuthor.Size = new System.Drawing.Size(405, 20);
            this.tbAuthor.TabIndex = 2;
            this.tbAuthor.TabStop = false;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(203, 75);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(28, 18);
            this.lblYear.TabIndex = 5;
            this.lblYear.Text = "Year";
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(294, 73);
            this.tbYear.MaxLength = 4;
            this.tbYear.Name = "tbYear";
            this.tbYear.NullText = "<Year>";
            this.tbYear.Size = new System.Drawing.Size(46, 20);
            this.tbYear.TabIndex = 4;
            this.tbYear.TabStop = false;
            this.tbYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.tbYear.GetChildAt(0).GetChildAt(0))).NullText = "<Year>";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(510, 73);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(38, 18);
            this.lblGroup.TabIndex = 7;
            this.lblGroup.Text = "Group";
            // 
            // tbGroup
            // 
            this.tbGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGroup.Location = new System.Drawing.Point(554, 73);
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.NullText = "<Group>";
            this.tbGroup.Size = new System.Drawing.Size(145, 20);
            this.tbGroup.TabIndex = 6;
            this.tbGroup.TabStop = false;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.tbGroup.GetChildAt(0).GetChildAt(0))).NullText = "<Group>";
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.Location = new System.Drawing.Point(346, 73);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(36, 18);
            this.lblGenre.TabIndex = 7;
            this.lblGenre.Text = "Genre";
            // 
            // tbGenre
            // 
            this.tbGenre.Location = new System.Drawing.Point(388, 73);
            this.tbGenre.Name = "tbGenre";
            this.tbGenre.NullText = "<Genre>";
            this.tbGenre.Size = new System.Drawing.Size(116, 20);
            this.tbGenre.TabIndex = 6;
            this.tbGenre.TabStop = false;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.tbGenre.GetChildAt(0).GetChildAt(0))).NullText = "<Genre>";
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(203, 101);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(60, 18);
            this.lblComments.TabIndex = 9;
            this.lblComments.Text = "Comments";
            // 
            // tbComments
            // 
            this.tbComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbComments.Location = new System.Drawing.Point(294, 99);
            this.tbComments.Name = "tbComments";
            this.tbComments.NullText = "<Comments>";
            this.tbComments.Size = new System.Drawing.Size(405, 20);
            this.tbComments.TabIndex = 8;
            this.tbComments.TabStop = false;
            // 
            // tbAudioDirectory
            // 
            this.tbAudioDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAudioDirectory.DialogType = Telerik.WinControls.UI.BrowseEditorDialogType.FolderBrowseDialog;
            this.tbAudioDirectory.Location = new System.Drawing.Point(294, 125);
            this.tbAudioDirectory.Name = "tbAudioDirectory";
            this.tbAudioDirectory.Size = new System.Drawing.Size(405, 20);
            this.tbAudioDirectory.TabIndex = 10;
            this.tbAudioDirectory.Text = "radBrowseEditor1";
            this.tbAudioDirectory.Value = "C:\\";
            // 
            // lblAudioDirectory
            // 
            this.lblAudioDirectory.AutoSize = true;
            this.lblAudioDirectory.Location = new System.Drawing.Point(203, 127);
            this.lblAudioDirectory.Name = "lblAudioDirectory";
            this.lblAudioDirectory.Size = new System.Drawing.Size(85, 18);
            this.lblAudioDirectory.TabIndex = 11;
            this.lblAudioDirectory.Text = "Audio Directory";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(579, 181);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            // 
            // 
            // 
            this.btnSave.RootElement.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnSave.Size = new System.Drawing.Size(120, 24);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddFromFile
            // 
            this.btnAddFromFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFromFile.Image = ((System.Drawing.Image)(resources.GetObject("btnAddFromFile.Image")));
            this.btnAddFromFile.Location = new System.Drawing.Point(453, 181);
            this.btnAddFromFile.Name = "btnAddFromFile";
            this.btnAddFromFile.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            // 
            // 
            // 
            this.btnAddFromFile.RootElement.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnAddFromFile.Size = new System.Drawing.Size(120, 24);
            this.btnAddFromFile.TabIndex = 13;
            this.btnAddFromFile.Text = "Add from file";
            this.btnAddFromFile.Click += new System.EventHandler(this.btnAddFromFile_Click);
            // 
            // ofd
            // 
            this.ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // pnlCover
            // 
            this.pnlCover.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCover.BackgroundImage")));
            this.pnlCover.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlCover.Location = new System.Drawing.Point(3, 25);
            this.pnlCover.Name = "pnlCover";
            this.pnlCover.Size = new System.Drawing.Size(194, 180);
            this.pnlCover.TabIndex = 14;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.pnlCover.GetChildAt(0).GetChildAt(1))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // btnImportCover
            // 
            this.btnImportCover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportCover.Image = ((System.Drawing.Image)(resources.GetObject("btnImportCover.Image")));
            this.btnImportCover.Location = new System.Drawing.Point(327, 181);
            this.btnImportCover.Name = "btnImportCover";
            this.btnImportCover.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            // 
            // 
            // 
            this.btnImportCover.RootElement.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnImportCover.Size = new System.Drawing.Size(120, 24);
            this.btnImportCover.TabIndex = 15;
            this.btnImportCover.Text = "Import Cover";
            this.btnImportCover.Click += new System.EventHandler(this.btnImportCover_Click);
            // 
            // btnTTS
            // 
            this.btnTTS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTTS.Image = ((System.Drawing.Image)(resources.GetObject("btnTTS.Image")));
            this.btnTTS.Location = new System.Drawing.Point(201, 181);
            this.btnTTS.Name = "btnTTS";
            this.btnTTS.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            // 
            // 
            // 
            this.btnTTS.RootElement.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnTTS.Size = new System.Drawing.Size(120, 24);
            this.btnTTS.TabIndex = 16;
            this.btnTTS.Text = "TTS";
            this.btnTTS.Click += new System.EventHandler(this.btnTTS_Click);
            // 
            // lblFileIndex
            // 
            this.lblFileIndex.AutoSize = true;
            this.lblFileIndex.Location = new System.Drawing.Point(203, 151);
            this.lblFileIndex.Name = "lblFileIndex";
            this.lblFileIndex.Size = new System.Drawing.Size(53, 18);
            this.lblFileIndex.TabIndex = 17;
            this.lblFileIndex.Text = "File index";
            // 
            // tbFileIndex
            // 
            this.tbFileIndex.Location = new System.Drawing.Point(294, 151);
            this.tbFileIndex.Name = "tbFileIndex";
            this.tbFileIndex.Size = new System.Drawing.Size(100, 20);
            this.tbFileIndex.TabIndex = 18;
            this.tbFileIndex.TabStop = false;
            this.tbFileIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // iVfDocumentModelBindingSource
            // 
            this.iVfDocumentModelBindingSource.DataSource = typeof(VfSpeaker.Data.Models.IVfDocumentModel);
            // 
            // VfDocumentEditView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 258);
            this.Controls.Add(this.tbFileIndex);
            this.Controls.Add(this.lblFileIndex);
            this.Controls.Add(this.btnTTS);
            this.Controls.Add(this.btnImportCover);
            this.Controls.Add(this.pnlCover);
            this.Controls.Add(this.btnAddFromFile);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblAudioDirectory);
            this.Controls.Add(this.tbAudioDirectory);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.tbComments);
            this.Controls.Add(this.lblGenre);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.tbGenre);
            this.Controls.Add(this.tbGroup);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.tbAuthor);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.tbName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VfDocumentEditView";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "VfDocumentEditView";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.tbName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuthor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAuthor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGenre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGenre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblComments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbComments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAudioDirectory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAudioDirectory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddFromFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImportCover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTTS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFileIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFileIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iVfDocumentModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox tbName;
        private Telerik.WinControls.UI.RadLabel lblName;
        private Telerik.WinControls.UI.RadLabel lblAuthor;
        private Telerik.WinControls.UI.RadTextBox tbAuthor;
        private Telerik.WinControls.UI.RadLabel lblYear;
        private Telerik.WinControls.UI.RadTextBox tbYear;
        private Telerik.WinControls.UI.RadLabel lblGroup;
        private Telerik.WinControls.UI.RadTextBox tbGroup;
        private Telerik.WinControls.UI.RadLabel lblGenre;
        private Telerik.WinControls.UI.RadTextBox tbGenre;
        private Telerik.WinControls.UI.RadLabel lblComments;
        private Telerik.WinControls.UI.RadTextBox tbComments;
        private Telerik.WinControls.UI.RadBrowseEditor tbAudioDirectory;
        private Telerik.WinControls.UI.RadLabel lblAudioDirectory;
        private Telerik.WinControls.UI.RadButton btnSave;
        private System.Windows.Forms.BindingSource iVfDocumentModelBindingSource;
        private Telerik.WinControls.UI.RadButton btnAddFromFile;
        private System.Windows.Forms.OpenFileDialog ofd;
        private Telerik.WinControls.UI.RadPanel pnlCover;
        private Telerik.WinControls.UI.RadButton btnImportCover;
        private Telerik.WinControls.UI.RadButton btnTTS;
        private Telerik.WinControls.UI.RadLabel lblFileIndex;
        private Telerik.WinControls.UI.RadTextBox tbFileIndex;
    }
}
