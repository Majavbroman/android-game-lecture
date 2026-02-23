using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Health Data", order = 1)]
public class HealthData : ScriptableObject
{
    [Serializable]
    public struct HealthType
    {
        public HeartType Type;
        public int Max;
        public int Start;
        public int Current;
    }

    [SerializeField] private HealthType[] _healthTypes;
}
