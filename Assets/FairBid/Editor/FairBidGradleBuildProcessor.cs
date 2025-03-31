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

    private const string FB_VERSION_PATTERN = "(\\d+\\.\\d+\\.\\d+(\\.\\d+)*)";

    public void OnPostGenerateGradleAndroidProject(string path)
    {
        // string rootPath = System.IO.Directory.GetParent(path).FullName;
        // string projectPath = Directory.GetCurrentDirectory();

        // Debug.Log($"FairBidGradleBuildProcessor - Found gradle project at path {rootPath}");

        // string fairBidSdkVersion = GetFairBidSdkVersion(projectPath);

        // if (String.IsNullOrEmpty(fairBidSdkVersion))
        // {
        //     Debug.Log("FairBidGradleBuildProcessor - Impossible to find a FairBid SDK Version - Please integrate plugin manually");
        //     return;
        // }

        // Debug.Log($"FairBidGradleBuildProcessor - Integrating FairBid SDK Plugin with version {fairBidSdkVersion}...");

        // AssurePluginIsApplied(rootPath);

        // AssurePluginIsDeclared(rootPath, fairBidSdkVersion);
    }

    private void AssurePluginIsApplied(string rootPath)
    {
        // Try to find the file in which the android application plugin is applied, because there is where the fairbid-sdk-plugin will go
        string[] gradleFiles = Directory.GetFiles(rootPath, "*.gradle", SearchOption.AllDirectories);
        string applicationPattern = "apply plugin: 'com\\.android\\.application'";

        foreach (string file in gradleFiles)
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

    private void AssurePluginIsDeclared(string rootPath, string fairBidSdkVersion)
    {
        // Try to find the root gradle script file, because there we can define the buildscript (or the plugins) block which will be propagated to the whole gradle project
        string[] baseGradleFiles = Directory.GetFiles(rootPath, "*.gradle");

        foreach (string file in baseGradleFiles)
        {
            string text = File.ReadAllText(file);
            if (!IsSettingGradle(file))
            {
                if (!InjectPluginInBaseGradleFile(file, text, fairBidSdkVersion))
                {
                    Debug.Log($"FairBidGradleBuildProcessor - No supported gradle file detected. This Unity Editor version and the respective gradle templates are not supported. Please contact Digital Turbine for support.");
                }
            }
        }
    }

    private bool InjectPluginInBaseGradleFile(string file, string fileContent, string fairBidSdkVersion)
    {
        if (DoesTextContainPattern(fileContent, "buildscript"))
        {
            Debug.Log($"FairBidGradleBuildProcessor - 'buildscript' block found! Proceeding with gradle < 7 plugin injection in base gradle file :\n {file}");
            InjectFairBidPluginInGradleLowerThan7(file, fileContent, fairBidSdkVersion);
            return true;
        }
        Debug.Log($"FairBidGradleBuildProcessor - 'buildscript' block not found!\nThis is likely an integration on Unity Editor >= 2022.3.\nTrying to find 'plugins' block in base file:\n {file}");
        if (DoesTextContainPattern(fileContent, "plugins {"))
        {
            Debug.Log($"FairBidGradleBuildProcessor - plugins block found. Proceeding with gradle 7 plugin injection in base gradle file:\n {file}");
            InjectFairBidPluginInGradle7(file, fileContent, fairBidSdkVersion);
            return true;
        }
        return false;
    }

    private bool IsSettingGradle(string file)
    {
        return file.Contains("settings");
    }

    private bool DoesTextContainPattern(string text, string pattern)
    {
        return Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase);
    }

    private void InjectFairBidPluginInGradleLowerThan7(string file, string text, string fairBidSdkVersion)
    {
        string pluginDeclaration = "com.fyber:fairbid-sdk-plugin:";
        string pluginDeclarationWithVersion = $"classpath '{pluginDeclaration}{fairBidSdkVersion}'";
        string contentToAdd = gradleClassicPluginDeclaration(pluginDeclarationWithVersion);
        string pluginDeclarationPattern = $"classpath ['|\"]{pluginDeclaration}{FB_VERSION_PATTERN}['|\"]";
        InjectPlugin(file, text, fairBidSdkVersion, pluginDeclarationPattern, pluginDeclarationWithVersion, contentToAdd);
    }

    private void InjectFairBidPluginInGradle7(string file, string text, string fairBidSdkVersion)
    {
        string pluginDeclaration = "id 'com.fyber.fairbid-sdk-plugin' version ";
        string pluginDeclarationWithVersion = $"{pluginDeclaration}'{fairBidSdkVersion}'";
        string contentToAdd = gradle7PluginDeclaration(pluginDeclarationWithVersion);
        string pluginDeclarationPattern = $"{pluginDeclaration}['|\"]{FB_VERSION_PATTERN}['|\"]";
        InjectPlugin(file, text, fairBidSdkVersion, pluginDeclarationPattern, pluginDeclarationWithVersion, contentToAdd);
    }

    private void InjectPlugin(string file, string text, string fairBidSdkVersion, string pluginDeclarationPattern, string pluginDeclarationWithVersion, string contentToAdd)
    {
        if (DoesTextContainPattern(text, pluginDeclarationPattern))
        {
            ReplacePluginVersion(file, text, fairBidSdkVersion, pluginDeclarationPattern, pluginDeclarationWithVersion);
        }
        else
        {
            Debug.Log($"FairBidGradleBuildProcessor - FairBid SDK Plugin declaration not found. Adding it to file {file}:\n{contentToAdd}");
            File.WriteAllText(file, contentToAdd + text);
        }

    }

    private string gradle7PluginDeclaration(string pluginDeclarationWithVersion)
    {
        return @$"
plugins {{
    {pluginDeclarationWithVersion}
}}
";
    }

    private string gradleClassicPluginDeclaration(string pluginDeclarationWithVersion)
    {
        return @$"
subprojects {{
    buildscript {{
        repositories {{
            mavenCentral()
        }}
        dependencies {{
            {pluginDeclarationWithVersion}
        }}
    }}
}}
";
    }

    private void ReplacePluginVersion(string file, string text, string fairBidSdkVersion, string pluginDeclarationPattern, string contentToInject)
    {
        // Sometimes we have a version suffix, so we ensure we don't override it because it is probably a debug build
        Match match = Regex.Match(text, pluginDeclarationPattern, RegexOptions.IgnoreCase);
        if (IsDevBuild(match))
        {
            string versionSuffix = match.Groups[2].Value;
            Debug.Log($"FairBidGradleBuildProcessor - Found FairBid SDK Plugin already declared in file {file} with suffix {versionSuffix} - Not modifying it...");
        }
        else
        {
            Debug.Log($"FairBidGradleBuildProcessor - Found FairBid SDK Plugin already declared in file {file} - Ensuring its version is {fairBidSdkVersion} by replacing the plugin declaration with:\n{contentToInject}");
            string contentWithFairBidPluginRightVersion = Regex.Replace(text, pluginDeclarationPattern, contentToInject);
            File.WriteAllText(file, contentWithFairBidPluginRightVersion);
        }
    }

    private bool IsDevBuild(Match match)
    {
        return match.Success && match.Groups.Count == 3 && !String.IsNullOrEmpty(match.Groups[2].Value);
    }

    private string GetFairBidSdkVersion(string projectPath)
    {
        // We need to get the version from the FairBid.cs, the line that points to the android compatible SDK version
        string[] fairbidFiles = Directory.GetFiles(projectPath, "FairBid.cs", SearchOption.AllDirectories);
        string fairbidSdkPattern = "private const string CompatibleAndroidVersion = \"(\\d+(\\.\\d+)+)\";";

        foreach (string file in fairbidFiles)
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
