using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(RectTransform))]
public class HealthUI : MonoBehaviour, IUIObject<IHealth>
{
    [SerializeField] private GameObject _heartPrefab;

    private readonly Dictionary<HeartType, List<HeartUI>> _hearts = new();

    private void Awake() {
        if (_heartPrefab == null)
        {
            Debug.LogError("HealthUI: Heart prefab is not assigned.");
            return;
        }
    }

    public async void Refresh(IHealth health)
    {
        Dictionary<HeartType, Health.Data> healthData = health.GetData();

        HeartType[] order = HeartData.HEART_ORDER;

        for (int i = 0; i < order.Length; i++)
        {
            HeartType type = order[i];
            if (!healthData.ContainsKey(type)) continue;

            Health.Data data = healthData[type];
            int heartAmount = Mathf.CeilToInt((float)data.Max / 2);
            List<HeartUI> hearts = _hearts.ContainsKey(type) ? _hearts[type] : new List<HeartUI>();

            while (hearts.Count != heartAmount)
            {
                Debug.Log($"Adjusting heart count for {type}: current {hearts.Count}, target {heartAmount}");

                if (hearts.Count < heartAmount)
                {
                    HeartUI newHeart = Instantiate(_heartPrefab, transform).GetComponent<HeartUI>();
                    hearts.Add(newHeart);
                }
                else
                {
                    Destroy(hearts[^1].gameObject);
                    hearts.RemoveAt(hearts.Count - 1);
                }
            }
            _hearts[type] = hearts;

            for (int j = 0; j < hearts.Count; j++)
            {
                if (j < data.Current / 2)
                {
                    await hearts[j].SetTexture(HeartState.Full, type);
                }
                else if (j == data.Current / 2 && data.Current % 2 == 1)
                {
                    await hearts[j].SetTexture(HeartState.Half, type);
                }
                else
                {
                    await hearts[j].SetTexture(HeartState.Empty, type);
                }
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
