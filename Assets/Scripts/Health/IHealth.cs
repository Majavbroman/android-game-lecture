using System.Threading.Tasks;
using UnityEngine;

public interface IHealth
{
    void Damage(int amount);
    void Heal(int amount);
    void SetHealth(int amount);
    void ResetHealth();
    int GetHealth();
    int GetMaxHealth();
    void SetData(HealthData data);
    HealthData GetData();
}
