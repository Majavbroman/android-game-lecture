using System.Threading.Tasks;
using UnityEngine;

public interface IHealth
{
    void ChangeHealth(int amount);
    void SetHealth(int amount);
    void ResetHealth();
    int GetHealth();
    int GetMaxHealth();
}
