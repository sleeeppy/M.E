using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Discord;
using System;
using System.Runtime;
using System.Threading.Tasks;

[InitializeOnLoad]
public class DiscordController
{
    private static Discord.Discord discord;
    private static string projectName { get {  return Application.productName; } }
    private static string version { get {  return Application.unityVersion; } }
    private static RuntimePlatform platform { get { return Application.platform; } }
    private static string activeSceneName {  get {  return EditorSceneManager.GetActiveScene().name; } }
    private static long lastTimeStamp;

    private const string applicationID = "1214444883020881990";
    static DiscordController()
    {
        DelayInit();
    }

    private static async void DelayInit(int delay = 1000)
    {
        await Task.Delay(delay);
        SetupDiscord();
    }

    private static void SetupDiscord()
    {
        discord = new Discord.Discord(long.Parse(applicationID), (ulong)CreateFlags.Default);
        lastTimeStamp = GetTimestamp();
        UpdateActivity();

        EditorApplication.update += EditorUpdate;
        EditorSceneManager.sceneOpened += SceneOpened;
    }

    private static void EditorUpdate()
    {
        discord.RunCallbacks();
    }

    private static void SceneOpened(UnityEngine.SceneManagement.Scene scene, OpenSceneMode sceneMode)
    {
        UpdateActivity();
    }

    private static void UpdateActivity()
    {
        ActivityManager activityManager = discord.GetActivityManager();
        Activity activity = new Activity
        {
            Details = "Editing " + projectName,
            State = activeSceneName + " | " + platform,
            Timestamps =
            {
                Start = lastTimeStamp
            },
            Assets =
            {
                LargeImage = "unitylogo",
                LargeText = version,
                SmallImage = "unity-dark-logo",
                SmallText = version
            }
        };

        activityManager.UpdateActivity(activity, result =>
        {
            Debug.Log("Discord result : " + result);
        });
    }

    private static long GetTimestamp()
    {
        long unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        return unixTimestamp;
    }
}
