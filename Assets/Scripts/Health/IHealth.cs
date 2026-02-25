using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IHealth
{
    void SetHealth(HealthData data);
    Dictionary<HeartType, Health.Data> GetData();
    Health.Data GetData(HeartType type);
    void ResetHealth();
}
