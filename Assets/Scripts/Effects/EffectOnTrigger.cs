using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnTrigger : MonoBehaviour
{
    [SerializeField] private List<Effect> _effects;

    private void OnTriggerEnter2D(Collider2D target)
    {
        foreach (var effect in _effects)
        {
            effect.ApplyEffect(target.transform);
        }

        Destroy(gameObject);
    }
}
