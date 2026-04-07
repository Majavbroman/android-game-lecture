using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [Serializable]
    public struct HealthDataEntry
    {
        public HeartType Type;
        public int Max;
        public int Current;
    }

    public float Score = 0;
    public float Position = 0;
    public List<HealthDataEntry> HealthData = new List<HealthDataEntry>();
    public float BulletsSpawned = 0;

    public void SetHealthData(Dictionary<HeartType, Health.Data> healthData)
    {
        HealthData.Clear();
        foreach (var kvp in healthData)
        {
            HealthData.Add(new HealthDataEntry { Type = kvp.Key, Max = kvp.Value.Max, Current = kvp.Value.Current });
        }
    }

    public Dictionary<HeartType, Health.Data> GetHealthData()
    {
        var healthData = new Dictionary<HeartType, Health.Data>();
        foreach (var entry in HealthData)
        {
            healthData[entry.Type] = new Health.Data { Max = entry.Max, Current = entry.Current };
        }
        return healthData;
    }
}
