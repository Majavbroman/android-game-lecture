using System.Collections.Generic;
using UnityEngine;

public class EffectOnMultiTap : MonoBehaviour
{
    [SerializeField] private int _requiredTaps = 2;
    [SerializeField] private float _tapWindow = 0.2f;
    private int _tapCount = 0;
    private float _tapBuffer = 0f;

    [SerializeField] private List<Effect> _effects;

    private void OnEnable() {
        InputReader.TapEvent += OnTap;
    }

    private void OnDisable() {
        InputReader.TapEvent -= OnTap;
    }

    private void Update()
    {
        _tapBuffer -= Time.deltaTime;

        if (_tapBuffer <= 0f)
        {
            _tapCount = 0;
        }
    }

    private void OnTap() {
        _tapCount++;

        if (_tapCount >= _requiredTaps)
        {
            TriggerEffects();
            _tapCount = 0;
        }

        _tapBuffer = _tapWindow;
    }

    private void TriggerEffects() {
        foreach (var effect in _effects)
        {
            effect.ApplyEffect(transform);
        }
    }
}
