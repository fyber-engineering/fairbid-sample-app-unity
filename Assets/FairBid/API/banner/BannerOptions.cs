//
// FairBid Unity SDK
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
using UnityEngine;
using System.Collections;

namespace Fyber
{
    public enum BannerRefreshMode
    {
        /// <summary>
        /// Auto.
        /// </summary>
        AUTO = 0,

        /// <summary>
        /// Manual.
        /// </summary>
        MANUAL,

        /// <summary>
        /// Disabled.
        /// </summary>
        OFF,
    }

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

        /// <summary>
        /// If set to `true` it requests AdMob, GAM, Google bidding banner with "adaptive size" 
        /// Default is `false`
        /// </summary>
        /// <returns>this <see cref="BannerOptions"/>.</returns>
        public BannerOptions SetAdaptive(bool adaptive)
        {
            this.adaptive = adaptive;
            return this;
        }

        /// <summary>
        /// Sets the banner refresh mode
        /// Default is `AUTO`
        /// </summary>
        /// <returns>this <see cref="BannerRefreshMode"/>.</returns>
        public BannerOptions SetRefreshMode(BannerRefreshMode refreshMode)
        {
            this.refreshMode = refreshMode;
            return this;
        }

        internal bool adaptive = false;
        internal string position = BannerOptions.POSITION_BOTTOM;
        internal BannerRefreshMode refreshMode = BannerRefreshMode.AUTO;
    }
}
