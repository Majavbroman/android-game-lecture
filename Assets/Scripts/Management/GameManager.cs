using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>, IDataSaver
{

    [SerializeField] private EventChannel<Empty> _onGameStart;
    [SerializeField] private EventChannel<Empty> _onGameEnd;
    [SerializeField] private EventChannel<GameData> _onGameResume;

    [SerializeField] TextMeshProUGUI _mainText;
    [SerializeField] TextMeshProUGUI _scoreText;

    private float _score = 0;
    private float _highestScore = 0;

    private void Start() {
        InputReader.TapEvent += HandleTap;
        PointChangeEffect.OnPointChange += ChangeScore;

        DataHandler.Instance.Register(this);
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
        if (DataHandler.Instance.GetData().SavedGameData(out var gameData))
        {
            _score = gameData.Score;
            _onGameResume.Invoke(gameData);
        }
        else
        {
            _score = 0;
            _onGameStart.Invoke(new Empty());
        }

        _mainText.text = "";
        _scoreText.text = $"{_score}";
        InputReader.TapEvent -= HandleTap;
    }

    private void OnApplicationQuit() {
        if (!DataHandler.Instance.GetData().SavedGameData(out _))
        {
            return;
        }
        
        DataHandler.Instance.SaveData();
    }

    public Task SaveData(ref SaveData saveData)
    {
        saveData.GameData.Score = _score;
        return Task.CompletedTask;
    }
}