using System;
using UnityEngine;

public class HealEffect : Effect
{
    [SerializeField] private int _amount;

    public override void ApplyEffect(Collider2D target)
    {
        if (target.TryGetComponent<IHealable>(out var healable))
        {
            healable.Heal(_amount);
        }
    }
}
