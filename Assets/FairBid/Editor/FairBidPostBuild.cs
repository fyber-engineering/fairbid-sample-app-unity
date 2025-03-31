using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
#endif

public class FairBidPostBuild
{
    #if UNITY_IOS
    [PostProcessBuild(101)]
    private static void OnPostProcessBuildPlayer(BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.iOS)
        {
            return;
        }

        UnityEngine.Debug.Log("FairBidPostBuild - started post-build script");

        string projectDir = Directory.GetCurrentDirectory();
        string fairBidSdkVersion = GetFairBidSdkVersion(projectDir);

        XcodeProject xcodeProject = new XcodeProject(pathToBuiltProject);
        FairBidPostBuild.UpdateProjectSettings(xcodeProject);
        xcodeProject.Save();
        xcodeProject.CreatePodProject(pathToBuiltProject, fairBidSdkVersion);

        UnityEngine.Debug.Log("FairBidPostBuild - finished post-build script");
    }

    private static void UpdateProjectSettings(XcodeProject xcodeProject)
    {
        xcodeProject.AddBuildProperty("OTHER_LDFLAGS", "-ObjC");
        xcodeProject.AddBuildProperty("CLANG_ENABLE_MODULES", "YES");
        xcodeProject.AddBuildProperty("VALIDATE_WORKSPACE", "YES");
    }

    private static string GetFairBidSdkVersion(string projectPath)
    {
        // We need to get the version from the FairBid.cs, the line that points to the iOS compatible SDK version
        string[] fairbidFiles = Directory.GetFiles(projectPath, "FairBid.cs", SearchOption.AllDirectories);
        string fairbidSdkPattern = "private const string CompatibleIOSVersion = \"(\\d+(\\.\\d+)+)\";";

        foreach(string file in fairbidFiles)
        {
            string text = File.ReadAllText(file);

            Match match = Regex.Match(text, fairbidSdkPattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string version = match.Groups[1].Value;
                UnityEngine.Debug.Log("FairBidPostBuild - Parsed FairBid SDK version " + version + " from file " + file);
                return version;
            }
        }

        return "";
    }
    #endif
}

class XcodeProject
{
    #if UNITY_IOS
    private PBXProject pbxProject;
    private string xcodeProjectPath;
    private string mainTargetGUID;
    private string unityFrameworkGUID;

    public XcodeProject(string projectPath)
    {
        xcodeProjectPath = projectPath + "/Unity-iPhone.xcodeproj/project.pbxproj";
        Open();
    }

    private void Open()
    {
        pbxProject = new PBXProject();
        pbxProject.ReadFromFile(xcodeProjectPath);
        mainTargetGUID = pbxProject.GetUnityMainTargetGuid();
        unityFrameworkGUID = pbxProject.GetUnityFrameworkTargetGuid();
    }

    public void Save()
    {
        pbxProject.WriteToFile(xcodeProjectPath);
    }

    public void CreatePodProject(string pathToBuiltProject, string fairBidSdkVersion)
    {
        string podfile = pathToBuiltProject + "/Podfile";
        if (!File.Exists(podfile))
        {
            UnityEngine.Debug.Log("Podfile does not exist. Creating one...");
            string podfileContent = $"use_frameworks!\nplatform :ios, '12.0'\n\ntarget 'Unity-iPhone' do\n\ttarget 'Unity-iPhone Tests' do\n\t\tinherit! :search_paths\n\tend\nend\n\ntarget 'UnityFramework' do\n\tpod 'FairBidSDK', '{fairBidSdkVersion}'\nend\n";
            File.WriteAllText(podfile, podfileContent);
        }
        else
        {
            UnityEngine.Debug.Log("Podfile exists. Checking its content...");
            string podfileContent = File.ReadAllText(podfile);
            string targetPattern = "target ['|\"]UnityFramework['|\"] do";

            if (Regex.IsMatch(podfileContent, targetPattern, RegexOptions.IgnoreCase))
            {
                UnityEngine.Debug.Log("Podfile contains UnityFramework target. Checking for FairBid SDK pod content...");
                string fairBidPodPattern = "pod ['|\"]FairBidSDK['|\"], ['|\"]\\d+(?:.\\d+)+['|\"]";

                if (Regex.IsMatch(podfileContent, fairBidPodPattern, RegexOptions.IgnoreCase))
                {
                    UnityEngine.Debug.Log($"Podfile contains UnityFramework target with FairBid SDK pod. Ensuring its version is {fairBidSdkVersion}...");
                    string contentWithPod = Regex.Replace(podfileContent, fairBidPodPattern, $"pod 'FairBidSDK', '{fairBidSdkVersion}'");
                    File.WriteAllText(podfile, contentWithPod);
                }
                else
                {
                    UnityEngine.Debug.Log("Podfile contains UnityFramework target but without FairBid SDK pod. Adding it...");
                    string podfileContentWithPod = Regex.Replace(podfileContent, targetPattern, $"target 'UnityFramework' do\n\tpod 'FairBidSDK', '{fairBidSdkVersion}'\n", RegexOptions.None);
                    File.WriteAllText(podfile, podfileContentWithPod);
                }
            }
            else
            {
                UnityEngine.Debug.Log("Podfile does not contain UnityFramework target. Adding it with FairBid SDK pod...");
                string toAdd = "\n\ntarget 'UnityFramework' do\n\tpod 'FairBidSDK', '{fairBidSdkVersion}'\nend\n";
                string fullContent = podfileContent + toAdd;
                File.WriteAllText(podfile, fullContent);
            }
        }
    }

    public void AddBuildProperty(string name, string value)
    {
        pbxProject.AddBuildProperty(mainTargetGUID, name, value);
        pbxProject.AddBuildProperty(unityFrameworkGUID, name, value);
    }

    public void SetBuildProperty(string name, string value)
    {
        pbxProject.SetBuildProperty(mainTargetGUID, name, value);
        pbxProject.SetBuildProperty(unityFrameworkGUID, name, value);
    }
    #endif
}
