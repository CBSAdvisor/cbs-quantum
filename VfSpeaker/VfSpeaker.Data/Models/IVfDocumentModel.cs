// ***********************************************************************
// Assembly         : VfSpeaker.Data
// Author           : Cameleer
// Created          : 12-14-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-18-2012
// ***********************************************************************
// <copyright file="IVfDocumentModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VfSpeaker.Data.Models
{
    /// <summary>
    /// Interface IVfDocumentModel
    /// </summary>
    public interface IVfDocumentModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the audio directory.
        /// </summary>
        /// <value>The audio directory.</value>
        string AudioDirectory { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        string Author { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        /// <value>The genre.</value>
        string Genre { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>The group.</value>
        string Group { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        int Year { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        string Comments { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets the index of the file.
        /// </summary>
        /// <value>The index of the file.</value>
        int FileIndex { get; set; }
    }
}
