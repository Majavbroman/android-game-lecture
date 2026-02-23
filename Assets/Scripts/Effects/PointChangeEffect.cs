using System;
using UnityEngine;

public class PointChangeEffect : Effect
{
    public static event Action<float> OnPointChange;

    [SerializeField] private float _amount;

    public override void ApplyEffect(Collider2D target)
    {
        if (target.CompareTag("Ground"))
        {
            OnPointChange?.Invoke(_amount);
        }
    }
}
