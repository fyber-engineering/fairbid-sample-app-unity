using UnityEngine;
using Fyber;
using System;
using UnityEngine.SceneManagement;

/*
 * The Main Scene,
 * responsible for starting the FairBid SDK and displaying the different ads - banner, interstitial, rewarded
 */
public class MainScene : MonoBehaviour
{
    /*
       * The app id provided through the Fyber console
       * "109613" can be used a sample application.
       * TODO replace with your own app id.
       */
    private const String PUBLISHER_APP_ID = "109613";

    void Start()
    {
        startFairBidSdk(PUBLISHER_APP_ID);
    }


    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /*
   * Helper method for initializing the SDK with the given app id
   * @param appId The app id provided through the Fyber console
   */
    private void startFairBidSdk(String appId)
    {
        FairBid.Start(appId);
    }

    /*
    * Helper method for showing the test suite
    */
    public void ShowTestSuite()
    {
        FairBid.ShowTestSuite();
    }
}
