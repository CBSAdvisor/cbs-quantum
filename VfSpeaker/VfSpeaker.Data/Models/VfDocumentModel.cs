// ***********************************************************************
// Assembly         : VfSpeaker.Data
// Author           : Cameleer
// Created          : 12-14-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-19-2012
// ***********************************************************************
// <copyright file="VfDocumentModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace VfSpeaker.Data.Models
{
    /// <summary>
    /// Class VfDocumentModel
    /// </summary>
    public class VfDocumentModel : IVfDocumentModel
    {
        #region Private data
        /// <summary>
        /// The file index
        /// </summary>
        private int fileIndex;
        #endregion

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="VfDocumentModel" /> class.
        /// </summary>
        public VfDocumentModel()
        { 
        }

        #region Public properties
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the audio directory.
        /// </summary>
        /// <value>The audio directory.</value>
        public string AudioDirectory { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        /// <value>The genre.</value>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>The group.</value>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments { get; set; }


        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets the index of the file.
        /// </summary>
        /// <value>The index of the file.</value>
        public int FileIndex
        {
            get { return fileIndex; }
            set
            {
                if (value != this.fileIndex)
                {
                    this.fileIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
