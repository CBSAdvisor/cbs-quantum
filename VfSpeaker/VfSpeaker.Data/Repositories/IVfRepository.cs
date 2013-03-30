// ***********************************************************************
// Assembly         : VfSpeaker.Data
// Author           : Cameleer
// Created          : 12-14-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-18-2012
// ***********************************************************************
// <copyright file="IVfRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VfSpeaker.Data.Models;

namespace VfSpeaker.Data.Repositories
{
    /// <summary>
    /// Interface IVfRepository
    /// </summary>
    public interface IVfRepository
    {
        /// <summary>
        /// Returns the data xml file path.
        /// </summary>
        /// <value>The data file path.</value>
        string DataFilePath { get; set; }

        /// <summary>
        /// Returns the data xml file path.
        /// </summary>
        /// <value>The data directory.</value>
        string DataDirectory { get; set; }


        /// <summary>
        /// Cache application data.
        /// </summary>
        void Initialize();

        #region Document management

        /// <summary>
        /// Adds the document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>Guid.</returns>
        Guid AddDocument(IVfDocumentModel document);

        /// <summary>
        /// Updates the document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>Guid.</returns>
        Guid UpdateDocument(IVfDocumentModel document);

        /// <summary>
        /// Gets all documents.
        /// </summary>
        /// <returns>List{IVfDocumentModel}.</returns>
        List<IVfDocumentModel> GetAllDocuments();

        /// <summary>
        /// Gets the document by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>IVfDocumentModel.</returns>
        IVfDocumentModel GetDocumentById(Guid id);
        
        #endregion

        #region Part management

        /// <summary>
        /// Adds the part.
        /// </summary>
        /// <param name="part">The part.</param>
        /// <returns>System.Int32.</returns>
        int AddPart(IVfPartModel part);

        /// <summary>
        /// Updates the part.
        /// </summary>
        /// <param name="part">The part.</param>
        /// <returns>System.Int32.</returns>
        int UpdatePart(IVfPartModel part);

        /// <summary>
        /// Gets the parts by document id.
        /// </summary>
        /// <param name="documentId">The document id.</param>
        /// <returns>List{IVfPartModel}.</returns>
        List<IVfPartModel> GetPartsByDocumentId(Guid documentId);

        /// <summary>
        /// Gets the parts by document id.
        /// </summary>
        /// <param name="documentId">The document id.</param>
        /// <param name="processed">if set to <c>true</c> [processed].</param>
        /// <returns>List{IVfPartModel}.</returns>
        List<IVfPartModel> GetPartsByDocumentId(Guid documentId, bool processed);

        #endregion

        /// <summary>
        /// Loads expense datastore data set from the data xml file.
        /// </summary>
        /// <returns>DataStore.</returns>
        DataStore LoadDataStore();

        /// <summary>
        /// Updates expense datastore dataset and stores its information to xml file.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        bool UpdateDataStore(DataStore ds);
    }
}
