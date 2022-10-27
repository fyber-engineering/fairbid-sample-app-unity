//
// FairBid Unity SDK
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
using UnityEngine;
using System.Collections;

namespace Fyber
{
    /// <summary>
    /// Class responsible to configure the banner display
    /// </summary>
    public class BannerOptions
    {
        /// <summary>
        /// Sets the position to this value to show ads at the top of the screen.
        /// </summary>
        private const string POSITION_TOP = "top";

        /// <summary>
        /// Sets the position to this value to show ads at the bottom of the screen.
        /// </summary>
        private const string POSITION_BOTTOM = "bottom";

        /// <summary>
        /// Displays the banner at the top of the screen
        /// </summary>
        /// <returns>this <see cref="BannerOptions"/>.</returns>
        public BannerOptions DisplayAtTheTop()
        {
            this.position = POSITION_TOP;
            return this;
        }

        /// <summary>
        /// Displays the banner at the bottom of the screen
        /// </summary>
        /// <returns>this <see cref="BannerOptions"/>.</returns>
        public BannerOptions DisplayAtTheBottom()
        {
            this.position = POSITION_BOTTOM;
            return this;
        }

        internal string position = BannerOptions.POSITION_BOTTOM;
    }
}
