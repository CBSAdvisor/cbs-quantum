// ***********************************************************************
// Assembly         : VfSpeaker
// Author           : Cameleer
// Created          : 12-16-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-16-2012
// ***********************************************************************
// <copyright file="IProgressCallback.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VfSpeaker
{
    /// <summary>
    /// Class IProgressCallback
    /// This defines an interface which can be implemented by UI elements
    /// which indicate the progress of a long operation.
    /// (See ProgressWindow for a typical implementation)
    /// </summary>
    public interface IProgressCallback
    {
        /// <summary>
        /// Call this method from the worker thread to initialize
        /// the progress callback.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        void Begin(int minimum, int maximum);

        /// <summary>
        /// Call this method from the worker thread to initialize
        /// the progress callback, without setting the range
        /// </summary>
        void Begin();

        /// <summary>
        /// Call this method from the worker thread to reset the range in the
        /// progress callback
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        void SetRange(int minimum, int maximum);

        /// <summary>
        /// Call this method from the worker thread to update the progress text.
        /// </summary>
        /// <param name="text">The text.</param>
        void SetComment(String text);

        /// <summary>
        /// Call this method from the worker thread to increase the progress
        /// counter by a specified value.
        /// </summary>
        /// <param name="val">The val.</param>
        void StepTo(int val);

        /// <summary>
        /// Call this method from the worker thread to step the progress meter to a
        /// particular value.
        /// </summary>
        /// <param name="val">The val.</param>
        void Increment(int val);

        /// <summary>
        /// If this property is true, then you should abort work
        /// </summary>
        /// <value><c>true</c> if this instance is aborting; otherwise, <c>false</c>.</value>
        bool IsAborting
        {
            get;
        }

        /// <summary>
        /// Call this method from the worker thread to finalize the progress meter
        /// </summary>
        void End();
    }
}
