using System;
using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private FallingObject _bulletPrefab;

    [Serializable]
    private struct Interval
    {
        public ObjectWave Wave;
        public int WaveEndThreshold;
    }

    [Header("Spawn Interval Settings")]
    [SerializeField] private Interval[] _spawnIntervals;
    private int _currentIntervalIndex = 0;
    private Func<bool> _changeIntervalCondition;

    private float _bulletsSpawned = 0;
    private float _spawnTimer;

    private bool _gameGoing = false;

    private void Start()
    {
        _changeIntervalCondition = () => _currentIntervalIndex < _spawnIntervals.Length - 1 && _bulletsSpawned >= _spawnIntervals[_currentIntervalIndex].WaveEndThreshold;
    }

    public void StartGame()
    {
        _gameGoing = true;
        _bulletsSpawned = 0;
        _currentIntervalIndex = 0;
        SetTimer();
    }
    public void EndGame() => _gameGoing = false;

    private void Update()
    {
        if (!_gameGoing) return;

        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            int bulletAmount = UnityEngine.Random.Range(1, _spawnIntervals[_currentIntervalIndex].Wave.MaxObjectSpawns + 1);
            StartCoroutine(SpawnBullet(bulletAmount));

            if (_changeIntervalCondition())
            {
                _currentIntervalIndex++;
            }

            SetTimer();
        }
    }

    private IEnumerator SpawnBullet(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float spawnX = UnityEngine.Random.Range(0.05f, 0.95f);
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(spawnX, 1.1f, 0));
            FallingObject @object = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);
            @object.SetObjectAmount(amount);
            _bulletsSpawned++;
            yield return new WaitForSeconds(0.15f * (amount - 1));
        }
    }

    private void SetTimer()
    {
        Interval currentInterval = _spawnIntervals[_currentIntervalIndex];
        _spawnTimer = UnityEngine.Random.Range(currentInterval.Wave.MinTimeBetweenSpawns, currentInterval.Wave.MaxTimeBetweenSpawns);
    }
}
