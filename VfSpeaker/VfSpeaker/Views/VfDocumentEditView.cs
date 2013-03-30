// ***********************************************************************
// Assembly         : VfSpeaker
// Created          : 12-14-2012
//
// Last Modified On : 12-14-2012
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using VfSpeaker.Data.Models;
using VfSpeaker.Data.Repositories;
using VfSpeaker.Extentions;

namespace VfSpeaker.Views
{
    /// <summary>
    /// Class VfDocumentEditView
    /// </summary>
    public partial class VfDocumentEditView : RadForm
    {
        private BindingSource _documentBindingSource = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="VfDocumentEditView" /> class.
        /// </summary>
        public VfDocumentEditView(Guid documentId)
        {
            InitializeComponent();

            _documentBindingSource = new BindingSource(this.components);

            ReloadView(documentId);
        }

        public void ReloadView(Guid documentId)
        {
            Document = VfApplication.CurrentContext.GetDocument(documentId);

            tbName.DataBindings.Clear();
            tbAuthor.DataBindings.Clear();
            tbYear.DataBindings.Clear();
            tbGenre.DataBindings.Clear();
            tbGroup.DataBindings.Clear();
            tbComments.DataBindings.Clear();
            tbAudioDirectory.DataBindings.Clear();
            tbFileIndex.DataBindings.Clear();

            _documentBindingSource.DataSource = Document;

            tbName.DataBindings.Add("Text", _documentBindingSource, "Title", true, DataSourceUpdateMode.OnPropertyChanged);
            tbAuthor.DataBindings.Add("Text", _documentBindingSource, "Author", true, DataSourceUpdateMode.OnPropertyChanged);
            tbYear.DataBindings.Add("Text", _documentBindingSource, "Year", true, DataSourceUpdateMode.OnPropertyChanged);
            tbGenre.DataBindings.Add("Text", _documentBindingSource, "Genre", true, DataSourceUpdateMode.OnPropertyChanged);
            tbGroup.DataBindings.Add("Text", _documentBindingSource, "Group", true, DataSourceUpdateMode.OnPropertyChanged);
            tbComments.DataBindings.Add("Text", _documentBindingSource, "Comments", true, DataSourceUpdateMode.OnPropertyChanged);
            tbAudioDirectory.DataBindings.Add("Value", _documentBindingSource, "AudioDirectory", true, DataSourceUpdateMode.OnPropertyChanged);
            tbFileIndex.DataBindings.Add("Text", _documentBindingSource, "FileIndex", true, DataSourceUpdateMode.OnPropertyChanged);

            UpdateCover();
   
        
        }

        /// <summary>
        /// Gets the document.
        /// </summary>
        /// <value>The document.</value>
        public IVfDocumentModel Document { get; private set; }

        private void btnSave_Click(object sender, EventArgs e)
        {
            VfApplication.CurrentContext.SaveDocument(Document);
        }

        private void btnAddFromFile_Click(object sender, EventArgs e)
        {
            ofd.InitialDirectory = VfRepository.DefaultDataDirectory;

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                String text = VfApplication.ReadTextFile(ofd.FileName);

                VfApplication.CurrentContext.AddText(Document.Id, text);
            }
        }

        private void btnImportCover_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = VfRepository.DefaultDataDirectory;

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        Document.Image = br.ReadBytes((int)fs.Length);
                        VfApplication.CurrentContext.SaveDocument(Document);
                        br.Close();
                    }
                    fs.Close();
                }
                UpdateCover();
            }
        }

        private void UpdateCover()
        {
            try
            {
                MemoryStream ms = new MemoryStream(Document.Image);
                pnlCover.BackgroundImage = Image.FromStream(ms);
            }
            catch(Exception)
            {
            
            }

            pnlCover.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void btnTTS_Click(object sender, EventArgs e)
        {
            VfApplication.CurrentContext.TextToSpeech(Document.Id);
        }
    }
}
