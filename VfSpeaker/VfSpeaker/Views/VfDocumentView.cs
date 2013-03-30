// ***********************************************************************
// Assembly         : VfSpeaker
// Author           : Cameleer
// Created          : 12-14-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-16-2012
// ***********************************************************************
// <copyright file="VfDocumentView.cs" company="">
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
using Telerik.WinControls.Themes;
using VfSpeaker.Data.Models;

namespace VfSpeaker.Views
{
    /// <summary>
    /// Class ControlForm
    /// </summary>
    public partial class VfDocumentView : RadForm
    {
        /// <summary>
        /// The _document edit view
        /// </summary>
        private VfDocumentEditView _documentEditView = null;

        /// <summary>
        /// The _parts view
        /// </summary>
        private VfPartsListView _partsView = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="VfDocumentView" /> class.
        /// </summary>
        /// <param name="documentId">The document id.</param>
        public VfDocumentView(Guid documentId)
        {
            InitializeComponent();

            DocumentId = documentId;
        }

        /// <summary>
        /// Handles the Load event of the VfDocumentView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void VfDocumentView_Load(object sender, EventArgs e)
        {
            DocumentEditView.Show();
            PartsView.Show();
        }

        /// <summary>
        /// Gets the document edit view.
        /// </summary>
        /// <value>The document edit view.</value>
        public VfDocumentEditView DocumentEditView
        {
            get
            {
                if (_documentEditView == null)
                {
                    _documentEditView = new VfDocumentEditView(DocumentId);
                    _documentEditView.TopLevel = false;
                    _documentEditView.Parent = this.DocumentPage;
                    _documentEditView.Dock = DockStyle.Fill;
                    this.DocumentPage.Controls.Add(_documentEditView);
                }

                return _documentEditView;
            }
        }

        /// <summary>
        /// Gets the parts view.
        /// </summary>
        /// <value>The parts view.</value>
        public VfPartsListView PartsView
        {
            get
            {
                if (_partsView == null)
                {
                    _partsView = new VfPartsListView(DocumentId);
                    _partsView.TopLevel = false;
                    _partsView.Parent = this.PartsPage;
                    _partsView.Dock = DockStyle.Fill;
                    this.PartsPage.Controls.Add(_partsView);
                }

                return _partsView;
            }
        }

        public void RefreshView()
        {
            DocumentEditView.ReloadView(DocumentId);
        }

        /// <summary>
        /// Gets the document.
        /// </summary>
        /// <value>The document.</value>
        public IVfDocumentModel Document
        {
            get
            {
                return VfApplication.CurrentContext.GetDocument(DocumentId);
            }
        }

        /// <summary>
        /// Gets the document id.
        /// </summary>
        /// <value>The document id.</value>
        public Guid DocumentId { get; private set; }
    }
}
