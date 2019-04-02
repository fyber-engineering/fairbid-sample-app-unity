//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

namespace Fyber
{
  public interface BannerListener
  {
    void OnError(string placementName, string error);
    void OnLoad(string placementName);
    void OnShow(string placementName);
    void OnClick(string placementName);
  }
}
