using UnityEngine;

public interface IHealth
{
    void ChangeHealth(int amount);
    int GetHealth();
    int GetMaxHealth();
}
