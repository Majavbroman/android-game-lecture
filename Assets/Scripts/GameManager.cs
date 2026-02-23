using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private EventChannel<Empty> OnGameStart;
    [SerializeField] private EventChannel<Empty> OnGameEnd;

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

        PointChangeEffect.OnPointChange += ChangeScore;
    }

    public void ChangeScore(float amount)
    {
        _score += amount;
        _scoreText.text = $"{_score}";
    }

    public void PlayerDied()
    {
        OnGameEnd.Invoke(new Empty());

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
        OnGameStart.Invoke(new Empty());

        InputReader.Instance.TapEvent -= HandleTap;
    }
}