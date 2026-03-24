using System;
using UnityEngine;

public class VibrateOnHealthLoss : MonoBehaviour {
    public void Vibrate(IHealth health)
    {
        HeartType type = health.GetLastHeartLost;

        if (type == HeartType.Orange)
        {
            Debug.Log("Vibrating for orange heart loss");
            VibrationHelper.Vibrate(50);
            return;
        }

        int totalHeartsLeft = 0;
        foreach (var data in health.GetData())
        {
            totalHeartsLeft += data.Value.Current;
        }

        int vibrationDuration = Math.Max(50, 500 - totalHeartsLeft * 75);
        VibrationHelper.Vibrate(vibrationDuration);
    }
}