using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Health : MonoBehaviour, IHealth, IDamagable, IHealable
{
    public class Data
    {
        public int Max;
        public int Current;
    }

    [SerializeField] private HealthData _healthDataAsset;
    private readonly Dictionary<HeartType, Data> _healthData = new();

    private Action _onDeath;

    [SerializeField] private EventChannel<IHealth> _healthEventChannel;

    private float _timeSinceLastDamage = 0f;

    private void Start()
    {
        if (_healthDataAsset != null)
        {
            ResetHealth();
        }

        GameManager gameManager = GameManager.Instance;
        _onDeath = gameManager.PlayerDied;
    }

    private void Update() {
        _timeSinceLastDamage += Time.deltaTime;
    }

    public void SetHealth(HealthData data)
    {
        _healthDataAsset = data;
        ResetHealth();
    }

    public void ResetHealth()
    {
        _healthData.Clear();

        foreach (var healthType in _healthDataAsset.HealthTypes)
        {
            _healthData[healthType.Type] = new Data
            {
                Max = healthType.Max,
                Current = healthType.Start
            };
        }

        _healthEventChannel.Invoke(this);
    }

    public Dictionary<HeartType, Data> GetData()
    {
        return _healthData;
    }

    public Data GetData(HeartType type)
    {
        try
        {
            return _healthData[type];
        }
        catch (KeyNotFoundException)
        {
            Debug.LogError($"Health type {type} not found.");
            return null;
        }
    }

    private bool IsDead()
    {
        return _healthData.Values.All(data => data.Current <= 0);
    }

    public void Damage(int amount)
    {
        Debug.Log($"Health: Taking damage: {amount}");
        if (IsDead()) return;

        HeartType[] order = HeartData.HEART_ORDER.Reverse().ToArray();

        for (int i = 0; i < order.Length; i++)
        {
            HeartType type = order[i];
            if (!_healthData.ContainsKey(type)) continue;

            Data data = _healthData[type];
            if (data.Current <= 0) continue;

            Debug.Log($"Applying damage to {type} heart: {amount} damage.");

            if (type == HeartType.Orange)
            {
                Debug.Log("Applying special damage logic for Orange heart.");

                data.Current -= data.Current % 2 == 0 ? 2 : 1;
                data.Max -= data.Max % 2 == 0 ? 2 : 1;

                break;
            }

            int damageToApply = Math.Min(amount, data.Current);
            data.Current -= damageToApply;
            amount -= damageToApply;

            if (amount <= 0) break;
        }

        if (IsDead())
        {
            _onDeath?.Invoke();
        }

        _timeSinceLastDamage = 0f;
        _healthEventChannel.Invoke(this);
    }

    public void Heal(int amount)
    {
        if (IsDead()) return;

        HeartType[] order = HeartData.HEART_ORDER;

        for (int i = 0; i < order.Length; i++)
        {
            HeartType type = order[i];
            if (!_healthData.ContainsKey(type)) continue;

            Data data = _healthData[type];
            if (data.Current >= data.Max) continue;

            int healToApply = Math.Min(amount, data.Max - data.Current);
            data.Current += healToApply;
            amount -= healToApply;

            if (amount <= 0) break;
        }
    }
}
