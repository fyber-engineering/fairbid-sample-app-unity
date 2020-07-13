Fyber's FairBid - Sample Unity application
============================================
This project demonstrates how to integrate Fyber's FairBid Unity Plugin in Unity.

Table of contents
=================

* [Prerequisites](#prerequisites)
* [Project Setup](#project-setup)
    * [Importing FairBid Unity Plugin](#importing-fairBid-unity-plugin)
        * [Set the target platform in Build Settings](#set-the-target-platform-in-build-settings)
            * [Additional Setup - Targeting Android P](#additional-setup---targeting-android-p)
        * [Platform specific setup](#platform-specific-setup)
* [Build and Run](#build-and-run)
* [Navigating the sample code](#navigating-the-sample-code)
* [Mediation](#mediation)
    * [Android](#android)
        * [Using External Dependency Manager for Unity - Android](#using-external-dependency-manager-for-unity---android)
        * [Using Gradle](#using-gradle)
        * [Additional step - Android Manifest](#additional-step---android-manifest)
    * [iOS](#ios)
        * [Using External Dependency Manager for Unity - iOS](#using-external-dependency-manager-for-unity---ios)
        * [Using Cocoapods](#using-cocoapods)
        * [Additional step - Info.plist](#additional-step---info.plist)
    * [Test Suite](#test-suite)
* [Support and documentation](#support-and-documentation)

### Prerequisites
* Unity Editor version 2018.*
* Android 4.1 (API level 16)+ (when building for Android)
* iOS 9.0+ (when building for iOS)

## Project Setup

### Importing FairBid Unity Plugin

Before building the project, you need to import the FaidBid Unity Plugin into this project. For this, you need to download FairBid Unity Plugin from [Fyber's dev portal](https://dev-unity.fyber.com/docs/integration).

With this project open in Unity, double-click the downloaded FairBid Unity Plugin package (FairBidSDK.unitypackage) file to import the FairBid Unity Plugin. When the Import Unity Package window opens, click All to select everything before importing.

#### Set the target platform in Build Settings

After the import is completed. You need to select the target platform (Android/iOS) you wish to run this project on and click on `File -> Build Settings`. 

##### Additional Setup - Targeting Android P

Refer to the section [Targeting Android P
](https://dev-unity.fyber.com/docs#targeting-android-p) for Android P setup instructions.

#### Platform specific setup

Refer to the [Step 1. Integration in Fyber's dev portal](https://dev-unity.fyber.com/docs#step-1--integration) for Android and iOS specific setups instructions.

## Build and Run

You're now ready to run this project. Click `File -> Build and Run`. Unity will build the project and run the *Sample Unity application* in the targeted platform (Android/iOS).

## Navigating the project code

In the folder `Assets/Scenes/`, we have the following scenes:
* [MainScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/MainScene.cs) - SDK is Initialized

* [BannerScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/BannerScene.cs) - Requesting Banner Ads
    
* [InterstitialScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/InterstitialScene.cs) - Requesting Interstitial Ads

* [RewardedScene](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Scenes/RewardedScene.cs) - Requesting Rewarded Ads

## Mediation

This project does not have any mediated network integrated in it. In order to integrate mediation to this project.

1. You need to add the 3rd party mediation network configuration from their dashboard to the Fyber's console. Refer to the section [Integrating Mediation Partners
](https://fyber-mediation.fyber.com/docs/integrating-mediation) in Fyber's dev portal for instructions.
2. After you have added the 3rd party mediation network configuration. You can integrate 3rd party networks in the project in two different approaches in both platforms (Android and iOS).

    - Using External Dependency Manager for Unity
    - Using Gradle and Cocoapods

 Follow the platform specific instructions below for the both approaches,

### Android

#### Using External Dependency Manager for Unity - Android

Follow the instructions for the section **Download the and import EDM4U** in [Fyber's dev portal](https://dev-unity.fyber.com/docs/integration) to download the External Dependency Manager for Unity(EDM4U).

After you have downloaded and imported the EDM4U in this project. You need to create a empty file with name - "FairBidMediationDependencies.xml" in the folder `Assets/Editor`. For more instructions follow the section "Declare mediated networks dependencies with EDM4U" in [Fyber's dev portal](https://dev-unity.fyber.com/docs/integration).

Navigate to the file [FairBidMediationDependencies.xml.template](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/FairBidMediationDependencies.xml.template) located in the root folder and and copy the commented out dependencies block for the networks you wish to integrate. Paste those dependencies block of networks in the file - "FairBidMediationDependencies.xml" in the folder `Assets/Editor`.

After completing the copy and parting of the dependenies block of networks. You need to run the dependency resolution by clicking on the menu `Assets > External Dependency Manager > Android Resolver > Resolve`. This will resolve the dependenies.

**Note:** If you have integrated several mediated networks in this project you need to uncommented the multidex entry in the file [mainTemplate.gradle](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Plugins/Android/mainTemplate.gradle)
```gradle
        multiDexEnabled true
```

#### Using Gradle

Navigate to the file [mainTemplate.gradle](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Plugins/Android/mainTemplate.gradle) located in the folder `Assets/Plugins/Android/` and uncomment the commented out dependencies block for the networks you wish to integrate.

Make sure the network versions you're integrating match the versions present in the [Fyber's dev portal](https://fyber-mediation.fyber.com/docs/supported-networks)

**Note:** If you have integrated several mediated networks in this project you need to uncommented the multidex entry in the file [mainTemplate.gradle](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Assets/Plugins/Android/mainTemplate.gradle)
```gradle
        multiDexEnabled true
```

#### Additional step - Android Manifest

Some networks may require you to add extra manifest entries.  
Double check them in [Fyber's dev portal](https://fyber-mediation.fyber.com/docs/integrating-mediation) and the integrated network's documentation to make sure you have the required entries.

Pay special attention to, 

* **AdMob** network - which will crash if certain configurations are missing.

* **Mintegral** network - Which still needs explicit declaration of Activities in the application manifest.

**Tip:** You can copy AdMob's and Mintegral entries from the file [AndroidManifest.xml.template](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/AndroidManifest.xml.template)

### iOS

#### Using External Dependency Manager for Unity - iOS

Navigate to the file [FairBidMediationDependencies.xml.template](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/FairBidMediationDependencies.xml.template) located in the root folder and and copy the commented out dependencies block for the networks you wish to integrate. Paste those dependencies block of networks in the file - "FairBidMediationDependencies.xml" in the folder `Assets/Editor`.

**Note:** If you don't have the file - "FairBidMediationDependencies.xml" in the folder `Assets/Editor`. Follow the instructions in the section "Declare mediated networks dependencies with EDM4U" in [Fyber's dev portal](https://dev-unity.fyber.com/docs/integration).

After completing the copy and parting of the dependenies block of networks. You need to run the dependency resolution by clicking on the menu `Assets > External Dependency Manager > iOS Resolver > Resolve`. This will resolve the dependenies.

#### Using Cocoapods

To integrate mediated networks on iOS. First, you need to Build the project. Then, within the generated XCode project's path, create a `Podfile`. 

In the `Podfile`, declare all pod dependencies for the mediated networks you wish to integrate.

Make sure the network versions you're integrating match the versions present in the [Fyber's dev portal](https://fyber-mediation.fyber.com/docs/supported-networks)

**Tip:** You can copy the [Podfile.template](https://github.com/Heyzap/fairbid-sample-app-unity/blob/master/Podfile.template) and adjust it to the mediated networks you wish to integrate.

After completion the declaration of pod dependencies in `Podfile`. You need to install the dependencies by running the following command in the terminal:   

`pod install --repo-update`.  

**Note:** Check (cocoapods.org)[https://cocoapods.org/] for more information, if you do not have cocoapods installed in your computer.

After completiong of the installation fo dependencies, open the generated XCode project workspace `.workspace`.

#### Additional step - Info.plist

In case you're integrating **AdMob** you'll need to add the following `Info.plist` entry of a `GADApplicationIdentifier` key with a string value of your AdMob app ID to avoid a crash on start:

```xml
<key>GADApplicationIdentifier</key>
<string>your AdMob app ID</string>
```

Check our [dev portal](https://fyber-mediation.fyber.com/docs/admob#infoplist) for more information

### Test Suite

Regardless of the platform you're running the sample on, you can always use the Test Suite to double check all the mediated networks were integrated successfully.


#### Support and documentation
Please visit our [documentation](https://dev-unity.fyber.com/docs)

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

