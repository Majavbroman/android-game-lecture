using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;
    
    private Func<bool> IsDead => () => _currentHealth <= 0;

    private Action _onDeath;

    [SerializeField] private EventChannel<IHealth> _healthEventChannel;


    private void Start()
    {
        _currentHealth = _maxHealth;

        GameManager gameManager = GameManager.Instance;
        _onDeath = gameManager.PlayerDied;
    }

    public void ChangeHealth(int amount)
    {
        if (IsDead()) return;

        _currentHealth += amount;

        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (IsDead())
        {
            _onDeath?.Invoke();
        }

        _healthEventChannel.Invoke(this);
    }

    public void SetHealth(int amount)
    {
        _currentHealth = Mathf.Clamp(amount, 0, _maxHealth);

        if (IsDead())
        {
            _onDeath?.Invoke();
        }

        _healthEventChannel.Invoke(this);
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        _healthEventChannel.Invoke(this);
    }

    public int GetHealth() => _currentHealth;
    public int GetMaxHealth() => _maxHealth;
}
