// ***********************************************************************
// Assembly         : VfSpeaker.Data
// Author           : Cameleer
// Created          : 12-16-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-17-2012
// ***********************************************************************
// <copyright file="IVfPartModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VfSpeaker.Data.Models
{
    /// <summary>
    /// Interface IVfPartModel
    /// </summary>
    public interface IVfPartModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        int Id { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        string Text { get; set; }
        /// <summary>
        /// Gets or sets the document id.
        /// </summary>
        /// <value>The document id.</value>
        Guid DocumentId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IVfPartModel" /> is processed.
        /// </summary>
        /// <value><c>true</c> if processed; otherwise, <c>false</c>.</value>
        bool Processed { get; set; }
    }
}
