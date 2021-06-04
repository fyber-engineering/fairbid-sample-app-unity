Fyber's FairBid - Sample Unity application
============================================
This sample app demonstrates the FairBid SDK integration best practices. It is aligned with the [FairBid developer documentation](https://developer.fyber.com/), which we recommend you to read as well.

Please note that when it comes to actually demonstrating the product "FairBid" as a monetization platform this sample app might fail due to the fact that ad delivery depends on factors like your country, your device your AAID/IDFA or any other kind of information that ad networks might have collected about you and that are taken into consideration when placing a bid to show an ad to you.

Table of contents
=================

* [Prerequisites](#prerequisites)
* [Project Setup](#project-setup)
   * [Integrating through Unity Package Manager](#integrating-through-unity-package-manager)
   * [Importing FairBid Unity Plugin raw package](#importing-fairbid-unity-plugin-raw-package)
* [Set the target platform in Build Settings](#set-the-target-platform-in-build-settings)
* [Build and Run](#build-and-run)
* [Navigating the project code](#navigating-the-project-code)
* [Mediation](#mediation)
   * [Test Suite](#test-suite)
* [Support and documentation](#support-and-documentation)
* [License](#license)

### Prerequisites
* Unity Editor version 2018.*
* Android 4.1 (API level 16)+ (when building for Android)
* iOS 9.0+ (when building for iOS)

## Project Setup

You have two different ways to integrate FairBid in this Unity project. Choose the one more convenient for you.

Please, note that this sample app is integrating FairBid Unity Plugin through the Unity Package Manager. If you want to use the raw Unity Package instead, the one manually downloaded from our developer portal, you will have to remove the dependency to the Unity Package Manager and native SDKs in order to avoid integrating them twice.

### Integrating through Unity Package Manager

**Note that this setup is the default for this sample app.**

We provide, for your convenience, our Unity Plugin as an [NPM package](https://www.npmjs.com/package/com.fyber.fairbid.unity). This package can be imported using the [Unity Package Manager](https://docs.unity3d.com/Manual/Packages.html).

To achieve that, a dependency to the FairBid Unity Plugin is added in the Package manifest file [here](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Packages/manifest.json#L12), along with the new entry in `scopedRegistries`, so that the dependency can be resolved.

   When opening Unity 3D, click on Window -> Package Manager and, in the drop down that lets you select among the repositories, choose `In Project`. You should be able to see Fyber FairBid integrated.

This lightweight dependency adds just the necessary sources to communicate with the FairBid SDK, but note that dependencies to the native SDKs are still necessary:

- Dependency to the FairBid Android SDK: Added to the already provided gradle template [here](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Assets/Plugins/Android/mainTemplate.gradle#L21).
- Dependency to the FairBid iOS SDK: Needs to be added manually by you in the exported Xcode project, inside your `Podfile`, in the same way it would be done in a native integration - check the documentation on how to edit the `Podfile` [here](https://developer.fyber.com/hc/en-us/articles/360010015478-iOS-SDK-Integration).

Be aware that this way of integrating Plugin & SDKs separately brings a lot of flexibility but it could cause incompatibility issues if you e. g. integrate an old version of the Unity Plugin with a newer version of a native SDK. The recommendation is to always use the latest versions announced in the developer portal.

### Importing FairBid Unity Plugin raw package

Before building the project, you need to import the FaidBid Unity Plugin. For this, you need to download FairBid Unity Plugin from [Fyber's developer portal](https://developer.fyber.com/hc/en-us/articles/360010151157-Unity-SDK-Integration).

Remember to remove the FairBid dependencies [here](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Packages/manifest.json#L12), [here](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Assets/Plugins/Android/mainTemplate.gradle#L21) and from your Podfile (if any) if importing the raw `.unitypackage` is your chosen build option.

With this project open in Unity, double-click the downloaded FairBid Unity Plugin package (`FairBidSDK.unitypackage`) file to import the FairBid Unity Plugin. When the *Import Unity Package* window opens, click `All` to select everything before importing.

## Set the target platform in Build Settings

After the import is completed, you need to select the target platform (Android/iOS) you wish to run this project on and click on `File -> Build Settings`.
Follow the [platform specific setup in our developer portal](https://developer.fyber.com/hc/en-us/articles/360010151157-Unity-SDK-Integration) 

## Build and Run

You're now ready to run this project. Click `File -> Build and Run`. Unity will build the project and run the *Sample Unity application* in the targeted platform (Android/iOS).

## Navigating the project code

In the folder `Assets/Scenes/`, we have the following scenes:
* SDK Initialization is located in [MainScene](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Assets/Scenes/MainScene.cs)
   * App id is defined [here](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Assets/Scenes/MainScene.cs#L33)
* Requesting Banner Ads - [BannerScene](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Assets/Scenes/BannerScene.cs)
   * Placement id for Banner Ads is defined [here](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Assets/Scenes/BannerScene.cs#L31)
* Requesting Interstitial Ads - [InterstitialScene](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Assets/Scenes/InterstitialScene.cs)
   * Placement id for Interstitial Ads is defined [here](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Assets/Scenes/InterstitialScene.cs#L31)
* Requesting Rewarded Ads - [RewardedScene](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Assets/Scenes/RewardedScene.cs)
   * Placement id for Rewarded Ads is defined [here](https://github.com/fyber-engineering/fairbid-sample-app-unity/blob/master/Assets/Scenes/RewardedScene.cs#L31)

## Mediation

If you want to test mediation features using this Sample app, there are a couple of steps you will need to take first: 

1. Add the 3rd party mediation network configuration from their dashboard to the Fyber's console. Refer to the section [Integrating Mediation Partners](https://developer.fyber.com/hc/en-us/articles/360010169358-Integrating-Mediation-Partners) in Fyber's developer portal for instructions.
2. Integrate 3rd party networks in the project. Go to the [mediation network table](https://developer.fyber.com/hc/en-us/articles/360010077777-Supported-Networks#platform-unity) and select the network you wish to integrate in the sample app. Then follow the instructions respective to the integration approach you choose. There are two different approaches to achieve this:

   - Using External Dependency Manager for Unity
   - Using Gradle and Cocoapods

### Test Suite

Regardless of the platform you're running the sample on, you can always use the Test Suite to double check all the mediated networks were integrated successfully.


### Support and documentation

Please visit our [documentation](https://developer.fyber.com/hc/en-us/articles/360010151157-Unity-SDK-Integration).

### License

Please, check [here](https://www.fyber.com/sdklicense).