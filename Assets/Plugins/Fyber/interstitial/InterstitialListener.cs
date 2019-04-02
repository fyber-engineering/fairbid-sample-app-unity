//
//
// Copyright (c) 2019 Fyber. All rights reserved.
//
//

namespace Fyber
{
  public interface InterstitialListener
  {
    void OnShow(string placementName);

    void OnClick(string placementName);

    void OnHide(string placementName);

    void OnShowFailure(string placementName);

    void OnAvailable(string placementName);

    void OnUnavailable(string placementName);

    void OnAudioStart(string placementName);

    void OnAudioFinish(string placementName);
  }
}
