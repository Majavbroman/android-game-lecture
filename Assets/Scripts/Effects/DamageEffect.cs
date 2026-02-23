using System;
using UnityEngine;

public class DamageEffect : Effect
{
    [SerializeField] private int _amount;

    public override void ApplyEffect(Collider2D target)
    {
        if (target.TryGetComponent<IHealth>(out var health))
        {
            health.Damage(_amount);
        }
    }
}
