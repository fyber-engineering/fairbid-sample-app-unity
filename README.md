Fyber's FairBid - Sample Unity application
============================================
This sample app demonstrates the FairBid SDK integration best practices. It is aligned with the FairBid developer documentation - developer.fyber.com, which we recommend you to read as well.

Please note that when it comes to actually demonstrating the product "FairBid" as a monetization platform this sample app might fail due to the fact that ad delivery depends on factors like your country, your device your AAID/IDFA or any other kind of information that ad networks might have collected about you and that are taken into consideration when placing a bid to show an ad to you.

Table of contents
=================

* [Prerequisites](#prerequisites)
* [Project Setup](#project-setup)
   * [Importing FairBid Unity Plugin](#importing-fairBid-unity-plugin)
       * [Set the target platform in Build Settings](#set-the-target-platform-in-build-settings)
       * [Platform specific setup](#platform-specific-setup)
* [Build and Run](#build-and-run)
* [Navigating the project code](#navigating-the-project-code)
* [Mediation](#mediation)
   * [Test Suite](#test-suite)
* [Support and documentation](#support-and-documentation)

### Prerequisites
* Unity Editor version 2018.*
* Android 4.1 (API level 16)+ (when building for Android)
* iOS 9.0+ (when building for iOS)

## Project Setup

### Importing FairBid Unity Plugin

Before building the project, you need to import the FaidBid Unity Plugin. For this, you need to download FairBid Unity Plugin from [Fyber's dev portal](https://developer.fyber.com/hc/en-us/articles/360010151157-Unity-SDK-Integration).

With this project open in Unity, double-click the downloaded FairBid Unity Plugin package (FairBidSDK.unitypackage) file to import the FairBid Unity Plugin. When the Import Unity Package window opens, click All to select everything before importing.

#### Set the target platform in Build Settings

After the import is completed, you need to select the target platform (Android/iOS) you wish to run this project on and click on `File -> Build Settings`.
Follow the [platform specific setup in our dev portal](https://developer.fyber.com/hc/en-us/articles/360010151157-Unity-SDK-Integration) 

## Build and Run

You're now ready to run this project. Click `File -> Build and Run`. Unity will build the project and run the *Sample Unity application* in the targeted platform (Android/iOS).

## Navigating the project code

In the folder `Assets/Scenes/`, we have the following scenes:
* SDK Initialization is located in [MainScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/MainScene.cs)
   * Appid is defined in line 33 of [MainScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/MainScene.cs)
* Requesting Banner Ads - [BannerScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/BannerScene.cs)
   * Placement id for Banner Ads is defined in line 31 [BannerScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/BannerScene.cs)
* Requesting Interstitial Ads - [InterstitialScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/InterstitialScene.cs)
   * Placement id for Interstitial Ads is defined in line 31 [InterstitialScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/InterstitialScene.cs)
* Requesting Rewarded Ads - [RewardedScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/RewardedScene.cs)
   * Placement id for Rewarded Ads is defined in line 31 [RewardedScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/RewardedScene.cs)

## Mediation

If you want to test mediation features using this Sample app, there are a couple of steps you will need to take first: 

1. Add the 3rd party mediation network configuration from their dashboard to the Fyber's console. Refer to the section [Integrating Mediation Partners
](https://developer.fyber.com/hc/en-us/articles/360010169358-Integrating-Mediation-Partners) in Fyber's dev portal for instructions.
2. Integrate 3rd party networks in the project. Go to the [mediation network table](https://developer.fyber.com/hc/en-us/articles/360010077777-Supported-Networks#platform-unity) and select the network you wish to integrate in the sample app. Then follow the instructions respective to the integration approach you choose. There are two different approaches to achieve this:.

   - Using External Dependency Manager for Unity
   - Using Gradle and Cocoapods

### Test Suite

Regardless of the platform you're running the sample on, you can always use the Test Suite to double check all the mediated networks were integrated successfully.


#### Support and documentation
Please visit our [documentation](https://developer.fyber.com/hc/en-us/articles/360010151157-Unity-SDK-Integration)

#### Project License

   Copyright (c) 2019-2020. Fyber N.V
  
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at
  
       http://www.apache.org/licenses/LICENSE-2.0
       
   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
