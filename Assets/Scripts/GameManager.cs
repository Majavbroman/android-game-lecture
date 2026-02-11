using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnPlayerDeath;
    public event Action OnGameStart;

    [SerializeField] TextMeshProUGUI _mainText;
    [SerializeField] TextMeshProUGUI _scoreText;

    private float _score = 0;
    private float _highestScore = 0;

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        InputReader.Instance.TapEvent += HandleTap;
    }

    public void AddScore(float amount)
    {
        _score += amount;
        _scoreText.text = $"{_score}";
    }

    public void PlayerDied()
    {
        OnPlayerDeath?.Invoke();

        if (_score > _highestScore)
        {
            _highestScore = _score;
        }

        _mainText.text = $"Final Score: {_score}\nHighest Score: {_highestScore}\nTap to Restart.";

        InputReader.Instance.TapEvent += HandleTap;
    }

    private void HandleTap()
    {
        _mainText.text = "";
        _score = 0;
        _scoreText.text = $"{_score}";
        OnGameStart?.Invoke();

        InputReader.Instance.TapEvent -= HandleTap;
    }
}