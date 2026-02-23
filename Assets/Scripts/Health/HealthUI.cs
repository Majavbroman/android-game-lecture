using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HealthUI : MonoBehaviour, IUIObject<IHealth>
{
    [SerializeField] private GameObject _heartPrefab;

    private readonly List<HeartUI> _hearts = new();

    private void Awake() {
        if (_heartPrefab == null)
        {
            Debug.LogError("HealthUI: Heart prefab is not assigned.");
            return;
        }
    }

    public async void Refresh(IHealth health)
    {
        int currentHealth = health.GetHealth();
        int maxHealth = health.GetMaxHealth();

        while (_hearts.Count < maxHealth / 2)
        {
            HeartUI newHeart = Instantiate(_heartPrefab, transform).GetComponent<HeartUI>();
            _hearts.Add(newHeart);
        }

        for (int i = 0; i < _hearts.Count; i++)
        {
            if (i < currentHealth / 2)
            {
                await _hearts[i].SetTexture(HeartState.Full);
            }
            else if (i == currentHealth / 2 && currentHealth % 2 == 1)
            {
                await _hearts[i].SetTexture(HeartState.Half);
            }
            else
            {
                await _hearts[i].SetTexture(HeartState.Empty);
            }
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
