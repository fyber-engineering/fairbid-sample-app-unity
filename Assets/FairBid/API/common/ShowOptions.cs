//
// FairBid Unity SDK
//
// Copyright (c) 2021 Fyber. All rights reserved.
//
using System.Collections;
using System.Collections.Generic;
using System;

namespace Fyber
{
    /// <summary>
    /// Provides a way to pass custom parameters that will be sent back to our servers when showing a fullscreen ad
    /// </summary>
    public class ShowOptions
    {

        public ShowOptions()
        {
            CustomParameters = null;
        }

        /// <summary>
        /// Initializes a ShowOptions object with a Dictionary of custom params to be passed to Server Side Rewarding upon video completion.
        /// You need to pass this object to the Show API every time you want to pass these parameters to Server Side Rewarding.
        /// </summary>
        /// <param name="customParameters"> A Dictionary<string, string> of parameters to be passed to Server Side Rewarding upon a video completion.
        ///                             Length of keys and values of custom parameters combined MUST NOT exceed 4096 characters. If the limit is exceeded,
        ///                             a null value will be passed to Server Side Rewarding upon video completion.</param>
        public ShowOptions(Dictionary<string, string> customParameters)
        {
            CustomParameters = customParameters;
        }

        /// <summary>
        /// Sets the Dictionary of custom parameters to be passed to Server Side Rewarding upon video completion.
        /// You need to pass this object through the Show API every time you want to pass these parameters to Server Side Rewarding.
        /// </summary>
        /// <param name="CustomParameters"> A Dictionary<string, string> of parameters to be passed to Server side rewarding upon a video completion.
        ///                             If the total number of chars in the Dictionary surpasses 4096,
        ///                             a null value will be passed to Server Side Rewarding upon video completion.</param>
        ///
        public Dictionary<string, string> CustomParameters { set; internal get; }

    }
}
