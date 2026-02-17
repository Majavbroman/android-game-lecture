using System;
using TMPro;
using UnityEngine;

public class Health
{
    private readonly int _maxHealth;
    private int _currentHealth;
    private Func<bool> _isDead => () => _currentHealth <= 0;

    [SerializeField] private TextMeshProUGUI _healthText;

    private readonly Action _onDeath;

    public Health(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;

        GameManager gameManager = GameManager.Instance;
        _onDeath = gameManager.PlayerDied;
    }

    public void StartGame()
    {
        _currentHealth = _maxHealth;
        UpdateHealthText();
    }

    public void ChangeHealth(int amount)
    {
        if (_isDead()) return;

        _currentHealth += amount;
        if (_isDead())
        {
            _currentHealth = 0;
            _onDeath?.Invoke();
        }

        UpdateHealthText();
    }   

    private void UpdateHealthText()
    {
        if (_healthText != null)
        {
            _healthText.color = Color.Lerp(Color.red, Color.green, (float)_currentHealth / _maxHealth);
            _healthText.text = $"Health: {_currentHealth}";
        }
    }
}
