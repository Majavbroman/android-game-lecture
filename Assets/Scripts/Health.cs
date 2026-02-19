using System;
using TMPro;
using UnityEngine;

public class Health : IHealth
{
    private readonly int _maxHealth;
    private int _currentHealth { 
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
        }
    }
    
    private Func<bool> IsDead => () => _currentHealth <= 0;

    private IUIObject _ui;

    private readonly Action _onDeath;

    public Health(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;

        GameManager gameManager = GameManager.Instance;
        _onDeath = gameManager.PlayerDied;
    }

    public void ChangeHealth(int amount)
    {
        if (IsDead()) return;

        _currentHealth += amount;

        if (IsDead())
        {
            _onDeath?.Invoke();
        }

        _ui?.Refresh();
    }

    public int GetHealth() => _currentHealth;
    public int GetMaxHealth() => _maxHealth;
}
