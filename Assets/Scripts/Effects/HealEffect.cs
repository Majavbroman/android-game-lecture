using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Scriptable Objects/Effects/Heal Effect")]
public class HealEffect : Effect
{
    [SerializeField] private int _amount;

    public override void ApplyEffect(Transform target)
    {
        if (target.TryGetComponent<IHealable>(out var healable))
        {
            healable.Heal(_amount);
        }
    }
}
