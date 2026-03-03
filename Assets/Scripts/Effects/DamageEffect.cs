using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Scriptable Objects/Effects/Damage Effect")]
public class DamageEffect : Effect
{
    [SerializeField] private int _amount;

    public override void ApplyEffect(Collider2D target)
    {
        if (target.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.Damage(_amount);
        }
    }
}
