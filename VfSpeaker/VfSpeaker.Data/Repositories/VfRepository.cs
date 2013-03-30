// ***********************************************************************
// Assembly         : VfSpeaker.Data
// Author           : Cameleer
// Created          : 12-14-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-08-2012
// ***********************************************************************
// <copyright file="VfRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VfSpeaker.Data.Models;

namespace VfSpeaker.Data.Repositories
{
    /// <summary>
    /// Class VfRepository
    /// </summary>
    public class VfRepository : IVfRepository
    {
        /// <summary>
        /// The ds
        /// </summary>
        private DataStore ds = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="VfRepository" /> class.
        /// </summary>
        public VfRepository()
        { 
        }

        /// <summary>
        /// Gets the default data file path.
        /// </summary>
        /// <value>The default data file path.</value>
        public static string DefaultDataFilePath
        {
            get
            {
                return DefaultDataDirectory + "book.xml";
            }
        }

        /// <summary>
        /// Gets the default data directory.
        /// </summary>
        /// <value>The default data directory.</value>
        public static string DefaultDataDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "data";
            }
        }

        /// <summary>
        /// Returns the data xml file path.
        /// </summary>
        /// <value>The data file path.</value>
        public string DataFilePath { get; set; }

        /// <summary>
        /// Returns the data xml file path.
        /// </summary>
        /// <value>The data directory.</value>
        public string DataDirectory { get; set; }

        /// <summary>
        /// Cache application data.
        /// </summary>
        public void Initialize()
        {
            if (ds == null)
            {
                ds = LoadDataStore();
            }
        }

        #region Document management

        /// <summary>
        /// Adds the document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>Guid.</returns>
        public Guid AddDocument(IVfDocumentModel document)
        {
            Initialize();

            DataStore.VfDocumentRow row = ds.VfDocument.NewVfDocumentRow();

            row.Id = Guid.NewGuid();
            row.Title = document.Title;
            ds.VfDocument.AddVfDocumentRow(row);

            UpdateDataStore(ds);

            return row.Id;
        }

        /// <summary>
        /// Updates the document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>Guid.</returns>
        public Guid UpdateDocument(IVfDocumentModel document)
        {
            Initialize();

            DataStore.VfDocumentRow row = ds.VfDocument.FindById(document.Id);

            UpdateDocumentEntity(document, row);

            UpdateDataStore(ds);

            return row.Id;
        }

        /// <summary>
        /// Gets all documents.
        /// </summary>
        /// <returns>List{IVfDocumentModel}.</returns>
        public List<IVfDocumentModel> GetAllDocuments()
        {
            var list = ds.VfDocument.Select<VfSpeaker.Data.DataStore.VfDocumentRow, IVfDocumentModel>(item => BuildDocumentModel(item));

            return new List<IVfDocumentModel>(list);
        }

        /// <summary>
        /// Gets the document by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>IVfDocumentModel.</returns>
        public IVfDocumentModel GetDocumentById(Guid id)
        {
            VfSpeaker.Data.DataStore.VfDocumentRow row = ds.VfDocument.FindById(id);

            return BuildDocumentModel(row);
        }

        #endregion

        #region
        public int AddPart(IVfPartModel part) {
            Initialize();

            DataStore.VfPartRow row = ds.VfPart.NewVfPartRow();

            UpdatePartEntity(part, row);

            ds.VfPart.AddVfPartRow(row);
            UpdateDataStore(ds);

            return row.Id;
        }

        public int UpdatePart(IVfPartModel part)
        {
            Initialize();

            DataStore.VfPartRow row = ds.VfPart.FindById(part.Id);

            UpdatePartEntity(part, row);
            UpdateDataStore(ds);

            return row.Id;
        }

        public List<IVfPartModel> GetPartsByDocumentId(Guid documentId)
        {
            Initialize();

            IEnumerable<IVfPartModel> parts = (from p in ds.VfPart
                                               join d in ds.VfDocument on p.DocumentId equals d.Id
                                               where p.DocumentId == documentId
                                               orderby p.DocumentId descending
                                               select BuildPartModel(p) );

            return new List<IVfPartModel>(parts);
        }

        public List<IVfPartModel> GetPartsByDocumentId(Guid documentId, bool processed)
        {
            Initialize();

            IEnumerable<IVfPartModel> parts = (from p in ds.VfPart
                                               join d in ds.VfDocument on p.DocumentId equals d.Id
                                               where p.DocumentId == documentId && p.Processed == processed
                                               orderby p.DocumentId descending
                                               select BuildPartModel(p));

            return new List<IVfPartModel>(parts);
        }

        #endregion

        /// <summary>
        /// Loads expense datastore data set from the data xml file.
        /// </summary>
        /// <returns>DataStore.</returns>
        public DataStore LoadDataStore()
        {
            DataStore ds = new DataStore();

            if (System.IO.File.Exists(this.DataFilePath))
            {
                ds.ReadXml(this.DataFilePath);
                ds.AcceptChanges();
            }
            else
            {
                UpdateDataStore(ds);
            }
            return ds;
        }

        /// <summary>
        /// Updates expense datastore dataset and stores its information to xml file.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool UpdateDataStore(DataStore ds)
        {
            if (ds.HasChanges())
            {
                if (!System.IO.Directory.Exists(DataDirectory))
                {
                    System.IO.Directory.CreateDirectory(DataDirectory);
                }
                ds.WriteXml(this.DataFilePath);
                ds.AcceptChanges();
                return true;
            }
            return false;
        }

        #region Private methods

        /// <summary>
        /// Builds the document model.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>VfDocumentModel.</returns>
        public VfDocumentModel BuildDocumentModel(VfSpeaker.Data.DataStore.VfDocumentRow row)
        {
            return new VfDocumentModel()
            {
                Id = row.Id,
                Title = row.Title,
                Author = row.Author,
                Genre = row.Genre,
                Group = row.Group,
                Year = row.Year,
                Comments = row.Comments,
                AudioDirectory = row.AudioDirectory,
                Image = Convert.FromBase64String(row.Image),
                FileIndex = row.FileIndex
            };
        }

        /// <summary>
        /// Updates the document entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="row">The row.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool UpdateDocumentEntity(IVfDocumentModel model, VfSpeaker.Data.DataStore.VfDocumentRow row)
        {
            row.Title = model.Title;
            row.Author = model.Author;
            row.Genre = model.Genre;
            row.Group = model.Group;
            row.Year = model.Year;
            row.Comments = model.Comments;
            row.AudioDirectory = model.AudioDirectory;
            row.Image = Convert.ToBase64String(model.Image);
            row.FileIndex = model.FileIndex;

            return true;
        }

        public IVfPartModel BuildPartModel(VfSpeaker.Data.DataStore.VfPartRow row)
        {
            return new VfPartModel()
            {
                Id = row.Id,
                Text = row.Text,
                DocumentId = row.DocumentId,
                Processed = row.Processed
            };
        }

        public bool UpdatePartEntity(IVfPartModel model, VfSpeaker.Data.DataStore.VfPartRow row)
        {
            row.Text = model.Text;
            row.DocumentId = model.DocumentId;
            row.Processed = model.Processed;

            return true;
        }

        #endregion
    }
}
