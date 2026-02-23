using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnTrigger : MonoBehaviour
{
    [SerializeField] private List<Effect> effects;

    private void OnTriggerEnter2D(Collider2D target)
    {
        foreach (var effect in effects)
        {
            effect.ApplyEffect(target);
        }
    }
}
