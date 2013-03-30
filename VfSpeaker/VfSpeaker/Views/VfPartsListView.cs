// ***********************************************************************
// Assembly         : VfSpeaker
// Author           : Cameleer
// Created          : 12-15-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-17-2012
// ***********************************************************************
// <copyright file="VfPartsListView.cs" company="">
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
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using VfSpeaker.Data.Models;

namespace VfSpeaker.Views
{
    /// <summary>
    /// Delegate AddListViewItemsInvoker
    /// </summary>
    /// <param name="values">The values.</param>
    public delegate void AddListViewItemsInvoker(ListViewDataItem item);

    /// <summary>
    /// Class VfPartsListView
    /// </summary>
    public partial class VfPartsListView : RadForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VfPartsListView" /> class.
        /// </summary>
        /// <param name="documentId">The document id.</param>
        public VfPartsListView(Guid documentId)
        {
            InitializeComponent();

            DocumentId = documentId;
            ResizeColumn(lvParts, lvParts.Columns[0]);
        }

        /// <summary>
        /// Resizes the column.
        /// </summary>
        /// <param name="lv">The lv.</param>
        /// <param name="column">The column.</param>
        private void ResizeColumn(RadListView lv, ListViewDetailColumn column)
        {
            column.Width = lv.ClientRectangle.Width - lv.Margin.All - 15;
        }

        /// <summary>
        /// Handles the Resize event of the lvParts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void lvParts_Resize(object sender, EventArgs e)
        {
            RadListView lv = (RadListView)sender;
            ResizeColumn(lv, lv.Columns[0]);
        }

        /// <summary>
        /// Handles the VisualItemFormatting event of the lvParts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ListViewVisualItemEventArgs" /> instance containing the event data.</param>
        private void lvParts_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        {
            e.VisualItem.TextWrap = true;

            if (!e.VisualItem.Selected && !((IVfPartModel)e.VisualItem.Data.Tag).Processed)
            {
                e.VisualItem.BorderColor = Color.Red;
                e.VisualItem.ForeColor = Color.Gray;
            }
            else
            {
                e.VisualItem.ResetValue(LightVisualElement.ForeColorProperty, Telerik.WinControls.ValueResetFlags.Local);
                e.VisualItem.ResetValue(LightVisualElement.BorderColorProperty, Telerik.WinControls.ValueResetFlags.Local);
            }
            //((IVfPartModel)e.VisualItem.Tag).Processed
            //e.VisualItem.NumberOfColors = 1;
            //e.VisualItem.BackColor = Color.Yellow;
            //e.VisualItem.BorderColor = Color.Blue;
        }

        /// <summary>
        /// Handles the CellCreating event of the lvParts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ListViewCellElementCreatingEventArgs" /> instance containing the event data.</param>
        private void lvParts_CellCreating(object sender, ListViewCellElementCreatingEventArgs e)
        {
            e.CellElement.TextWrap = true;
        }

        /// <summary>
        /// Updates the list view.
        /// </summary>
        private void UpdateListView()
        {
            ProgressForm prForm = new ProgressForm();

            ThreadPool.QueueUserWorkItem(new WaitCallback(DoUpdateListView), prForm);

            prForm.ShowDialog(this);
        }

        /// <summary>
        /// Does the update list view.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        private void DoUpdateListView(object parameters) 
        {
            IProgressCallback progressCallback = (IProgressCallback)parameters;

            progressCallback.Begin();
            progressCallback.StepTo(0);
            progressCallback.SetComment("Getting parts list...");

            List<IVfPartModel> parts = VfApplication.CurrentContext.GetParts(DocumentId);
            progressCallback.SetRange(0, parts.Count);

            parts.ForEach((item) => {

                ListViewDataItem lvItem = new ListViewDataItem();
                lvItem.Key = item.Id;
                lvItem.Text = String.Format("Item-{0:00000}", item.Id);
                lvItem.Tag = item;
                lvItem.SubItems.Add(item.Text);

                Invoke(new AddListViewItemsInvoker(lvParts.Items.Add), new object[] { lvItem });
                
                if (progressCallback.IsAborting)
                {
                    return;
                }
                progressCallback.SetComment(item.Text);
                progressCallback.Increment(1);
            });

            progressCallback.End();
        }

        /// <summary>
        /// Gets the document id.
        /// </summary>
        /// <value>The document id.</value>
        public Guid DocumentId { get; private set; }

        /// <summary>
        /// Handles the Load event of the VfPartsListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void VfPartsListView_Load(object sender, EventArgs e)
        {
            UpdateListView();
        }

        /// <summary>
        /// Handles the CellFormatting event of the lvParts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ListViewCellFormattingEventArgs" /> instance containing the event data.</param>
        private void lvParts_CellFormatting(object sender, ListViewCellFormattingEventArgs e)
        {
            e.CellElement.TextWrap = true;
        }
    }
}
