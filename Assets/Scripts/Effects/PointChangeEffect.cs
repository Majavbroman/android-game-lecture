using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PointChangeEffect", menuName = "Scriptable Objects/Effects/Point Change Effect")]
public class PointChangeEffect : Effect
{
    public static event Action<float> OnPointChange;

    [SerializeField] private float _amount;
    [SerializeField] private string[] _affectedTags;

    public override void ApplyEffect(Transform target)
    {
        foreach (string tag in _affectedTags)
        {
            if (!target.CompareTag(tag)) continue;

            OnPointChange?.Invoke(_amount);
            break;
        }
    }
}
