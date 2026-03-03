using System;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private EventChannel<Empty> _onGameStart;
    [SerializeField] private EventChannel<Empty> _onGameEnd;

    [SerializeField] TextMeshProUGUI _mainText;
    [SerializeField] TextMeshProUGUI _scoreText;

    private float _score = 0;
    private float _highestScore = 0;

    private void Start() {
        InputReader.TapEvent += HandleTap;
        PointChangeEffect.OnPointChange += ChangeScore;
    }

    public void ChangeScore(float amount)
    {
        _score += amount;
        _scoreText.text = $"{_score}";
    }

    public void PlayerDied()
    {
        _onGameEnd.Invoke(new Empty());

        if (_score > _highestScore)
        {
            _highestScore = _score;
        }

        _mainText.text = $"Final Score: {_score}\nHighest Score: {_highestScore}\nTap to Restart.";

        InputReader.TapEvent += HandleTap;
    }

    private void HandleTap()
    {
        _mainText.text = "";
        _score = 0;
        _scoreText.text = $"{_score}";
        _onGameStart.Invoke(new Empty());

        InputReader.TapEvent -= HandleTap;
    }
}