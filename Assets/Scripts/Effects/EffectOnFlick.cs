using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnFlick : MonoBehaviour
{
    [SerializeField] private float _flickThreshold = 100f;
    
    [SerializeField] private List<Effect> _effects;

    private void OnEnable() {
        InputReader.TouchDeltaEvent += CheckFlick;
    }

    private void OnDisable() {
        InputReader.TouchDeltaEvent -= CheckFlick;
    }

    private void CheckFlick(Vector2 delta) {
        if (Math.Abs(delta.y) > _flickThreshold)
        {
            TriggerEffects();
        }
    }

    private void TriggerEffects() {
        Debug.Log("Flick Effect Triggered!");
        foreach (var effect in _effects)
        {
            effect.ApplyEffect(transform);
        }
    }
}
