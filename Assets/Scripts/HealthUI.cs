using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HealthUI : MonoBehaviour, IUIObject
{
    [SerializeField] private GameObject _heartPrefab;

    private IHealth _health;
    private List<HealthHeart> _hearts = new List<HealthHeart>();

    private RectTransform _rectTransform;

    private void Awake() {
        if (_heartPrefab == null)
        {
            Debug.LogError("HealthUI: Heart prefab is not assigned.");
        }

        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        UIManager uiManager = UIManager.Instance;
    }


    public void Initialize(IHealth health)
    {
        _health = health;
        Refresh();
    }

    public void Refresh()
    {
        int currentHealth = _health.GetHealth();
        int maxHealth = _health.GetMaxHealth();

        while (_hearts.Count < maxHealth / 2)
        {
            Vector3 spawnPosition = _rectTransform.position + new Vector3(_hearts.Count * 120, 0, 0);
            HealthHeart newHeart = Instantiate(_heartPrefab, spawnPosition, Quaternion.identity, transform).GetComponent<HealthHeart>();
            _hearts.Add(newHeart);
        }
    }

    public void Show()
    {
        throw new System.NotImplementedException();
    }

    public void Hide()
    {
        throw new System.NotImplementedException();
    }
}
