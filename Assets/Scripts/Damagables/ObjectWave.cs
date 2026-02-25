using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Object Wave", order = 1)]
public class ObjectWave : ScriptableObject
{
    [Serializable]
    public class Object
    {
        public FallingObject Obj;
        [Range(0, 100)] public int Probability;
    }

    public Object[] AvailableObjects;
    public float MinTimeBetweenSpawns;
    public float MaxTimeBetweenSpawns;
    [Range(1, 10)] public int MinObjectSpawns;
    [Range(1, 10)] public int MaxObjectSpawns;

    private void OnValidate()
    {
        if (MinTimeBetweenSpawns > MaxTimeBetweenSpawns)
        {
            MaxTimeBetweenSpawns = MinTimeBetweenSpawns;
        }

        if (MinObjectSpawns > MaxObjectSpawns)
        {
            MaxObjectSpawns = MinObjectSpawns;
        }
        
        if (AvailableObjects == null || AvailableObjects.Length == 0) return;

        int totalProbability = 0;
        foreach (var obj in AvailableObjects)
        {
            totalProbability += obj.Probability;
            if (totalProbability > 100)
            {
                obj.Probability = 100 - (totalProbability - obj.Probability);
                totalProbability = 100;
            }
        }
    }
}
