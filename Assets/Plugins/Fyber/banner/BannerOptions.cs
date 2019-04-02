//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

using UnityEngine;
using System.Collections;

namespace Fyber {

  /// <summary>
  /// A set of options that describes how to show a banner ad.
  /// </summary>
  public class BannerOptions {

    /// <summary>
    /// Set `HZBannerShowOptions.Position` to this value to show ads at the top of the screen.
    /// </summary>
    private const string POSITION_TOP = "top";
    /// <summary>
    /// Set `HZBannerShowOptions.Position` to this value to show ads at the bottom of the screen.
    /// </summary>
    private const string POSITION_BOTTOM = "bottom";

    public BannerOptions DisplayAtTheTop()
    {
      this.position = POSITION_TOP;
      return this;
    }

    public BannerOptions DisplayAtTheBottom()
    {
      this.position = POSITION_BOTTOM;
      return this;
    }

    internal string position = BannerOptions.POSITION_BOTTOM;
  }
}
