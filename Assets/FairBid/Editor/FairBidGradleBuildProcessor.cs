using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Android;
using UnityEditor.Callbacks;
using UnityEngine;

class FairBidGradleBuildProcessor : IPostGenerateGradleAndroidProject
{
    public int callbackOrder { get { return 0; } }

    public void OnPostGenerateGradleAndroidProject(string path)
    {
        string rootPath = path + "/..";
        string projectDir = Directory.GetCurrentDirectory();

        Debug.Log($"FairBidGradleBuildProcessor - Found gradle project at path {rootPath}");

        string fairBidSdkVersion = GetFairBidSdkVersion(projectDir);

        if (String.IsNullOrEmpty(fairBidSdkVersion))
        {
            Debug.Log("FairBidGradleBuildProcessor - Impossible to find a FairBid SDK Version - Please integrate plugin manually");
            return;
        }

        Debug.Log($"FairBidGradleBuildProcessor - Integrating FairBid SDK Plugin with version {fairBidSdkVersion}...");

        assurePluginInclusion(rootPath);

        assurePluginClasspathInclusion(rootPath, fairBidSdkVersion);
    }

    private void assurePluginInclusion(string rootPath)
    {
        // Try to find the file in which the android application plugin is applied, because there is where the fairbid-sdk-plugin will go
        string[] gradleFiles = Directory.GetFiles(rootPath, "*.gradle", SearchOption.AllDirectories);
        string applicationPattern = "apply plugin: 'com\\.android\\.application'";

        foreach(string file in gradleFiles)
        {
            string text = File.ReadAllText(file);

            // application plugin found
            if (Regex.IsMatch(text, applicationPattern, RegexOptions.IgnoreCase))
            {
                string fairbidSdkPluginPattern = "apply plugin: ['|\"]com\\.fyber\\.fairbid-sdk-plugin['|\"]";

                if (Regex.IsMatch(text, fairbidSdkPluginPattern, RegexOptions.IgnoreCase))
                {
                    Debug.Log($"FairBidGradleBuildProcessor - FairBid SDK Plugin already applied in file {file}");

                    // Ensuring that it is not applied with double quotes
                    string contentWithPlugin = Regex.Replace(text, fairbidSdkPluginPattern, "apply plugin: 'com.fyber.fairbid-sdk-plugin'", RegexOptions.None);
                    File.WriteAllText(file, contentWithPlugin);
                }
                else
                {
                    Debug.Log($"FairBidGradleBuildProcessor - FairBid SDK Plugin not found. Applying it to file {file}");

                    string contentWithPlugin = Regex.Replace(text, applicationPattern, "apply plugin: 'com.android.application'\napply plugin: 'com.fyber.fairbid-sdk-plugin'", RegexOptions.None);
                    File.WriteAllText(file, contentWithPlugin);
                }
            }
        }
    }

    private void assurePluginClasspathInclusion(string rootPath, string fairBidSdkVersion)
    {
        // Try to find the root gradle script file, because there we can define the buildscript block which will be propagated to the whole gradle project
        string[] baseGradleFiles = Directory.GetFiles(rootPath, "*.gradle");
        string buildscriptPattern = "buildscript";

        foreach(string file in baseGradleFiles)
        {
            string text = File.ReadAllText(file);

            if (Regex.IsMatch(text, buildscriptPattern, RegexOptions.IgnoreCase))
            {
                // Capture M.m.p and consider an extra group that might be a suffix
                string classpathPattern = "classpath ['|\"]com.fyber:fairbid-sdk-plugin:(\\d+\\.\\d+\\.\\d+(\\.\\d+)*)['|\"]";

                if (Regex.IsMatch(text, classpathPattern, RegexOptions.IgnoreCase))
                {
                    // Sometimes we have a version suffix, so we ensure we don't override it because it is probably a debug build
                    Match match = Regex.Match(text, classpathPattern, RegexOptions.IgnoreCase);
                    if (match.Success && match.Groups.Count == 3 && !String.IsNullOrEmpty(match.Groups[2].Value))
                    {
                        string versionSuffix = match.Groups[2].Value;
                        Debug.Log($"FairBidGradleBuildProcessor - Found FairBid SDK Plugin classpath already applied in file {file} with suffix {versionSuffix} - Not modifying it...");
                    } else {
                        Debug.Log($"FairBidGradleBuildProcessor - Found FairBid SDK Plugin classpath already applied in file {file} - Ensuring its version is {fairBidSdkVersion}...");
                        string contentWithClasspath = Regex.Replace(text, classpathPattern, $"classpath 'com.fyber:fairbid-sdk-plugin:{fairBidSdkVersion}'");
                        File.WriteAllText(file, contentWithClasspath);
                    }
                }
                else
                {
                    Debug.Log($"FairBidGradleBuildProcessor - FairBid SDK Plugin classpath not found. Applying it to file {file}");

                    // write the classpath into the base gradle file - it's not a problem if the file already has a buildscript block, because gradle lets you add it more than once
                    string contentWithClasspath = $"subprojects {{\n\tbuildscript {{\n\t\trepositories {{\n\t\t\tmavenCentral()\n\t\t}}\n\t\tdependencies {{\n\t\t\tclasspath 'com.fyber:fairbid-sdk-plugin:{fairBidSdkVersion}'\n\t\t}}\n\t}}\n}}\n" + text;
                    File.WriteAllText(file, contentWithClasspath);
                }
            }
        }
    }

    private string GetFairBidSdkVersion(string projectPath)
    {
        // We need to get the version from the FairBid.cs, the line that points to the android compatible SDK version
        string[] fairbidFiles = Directory.GetFiles(projectPath, "FairBid.cs", SearchOption.AllDirectories);
        string fairbidSdkPattern = "private const string CompatibleAndroidVersion = \"(\\d+(\\.\\d+)+)\";";

        foreach(string file in fairbidFiles)
        {
            string text = File.ReadAllText(file);

            Match match = Regex.Match(text, fairbidSdkPattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string version = match.Groups[1].Value;
                Debug.Log("FairBidGradleBuildProcessor - Parsed FairBid SDK version " + version + " from file " + file);
                return version;
            }
        }

        return "";
    }
}
