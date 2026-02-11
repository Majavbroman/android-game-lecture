using System;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;
    private bool _isDead => _currentHealth <= 0;

    [SerializeField] private TextMeshProUGUI _healthText;

    private Action _onDeath;

    private void Start()
    {
        GameManager.Instance.OnGameStart += StartGame;
        _onDeath += () => GameManager.Instance.PlayerDied();
    }

    public void StartGame()
    {
        _currentHealth = _maxHealth;
        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;
        if (_isDead)
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
