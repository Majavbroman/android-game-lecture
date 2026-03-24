using UnityEngine;
using System;

public static class VibrationHelper
{
#if UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaObject vibrator;

    static VibrationHelper()
    {
        // Get the vibrator system service from Unity's current activity
        var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    }

    // Vibrate for a specific duration (milliseconds)
    public static void Vibrate(long milliseconds)
    {
        if (vibrator != null)
        {
            vibrator.Call("vibrate", milliseconds);
        }
    }

    // Vibrate with a pattern
    // pattern: array of [wait, vibrate, wait, vibrate, ...] in milliseconds
    // repeat: index to repeat from, -1 = no repeat
    public static void VibratePattern(long[] pattern, int repeat = -1)
    {
        if (vibrator != null)
        {
            vibrator.Call("vibrate", pattern, repeat);
        }
    }
#else
    // Fallback for editor / iOS
    public static void Vibrate(long milliseconds) => Handheld.Vibrate();
    public static void VibratePattern(long[] pattern, int repeat = -1) => Handheld.Vibrate();
#endif
}