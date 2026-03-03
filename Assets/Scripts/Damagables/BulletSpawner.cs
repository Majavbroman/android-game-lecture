using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Serializable]
    private struct Interval
    {
        public ObjectWave Wave;
        public int WaveEndThreshold;
    }

    [Header("Spawn Interval Settings")]
    [SerializeField] private Interval[] _spawnIntervals;
    private Queue<Interval> _intervalQueue = new Queue<Interval>();
    private Func<bool> _changeIntervalCondition;

    private float _bulletsSpawned = 0;
    private float _spawnTimer;

    private bool _gameGoing = false;

    private void Awake() {
        foreach (var interval in _spawnIntervals)
        {
            _intervalQueue.Enqueue(interval);
        }

        _spawnIntervals = null;
    }

    private void Start()
    {
        _changeIntervalCondition = () => _intervalQueue.Count > 1 && _bulletsSpawned >= _intervalQueue.Peek().WaveEndThreshold;
    }

    public void StartGame()
    {
        _gameGoing = true;
        _bulletsSpawned = 0;
        SetTimer();
    }
    public void EndGame() => _gameGoing = false;

    private void Update()
    {
        if (!_gameGoing) return;

        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            int bulletAmount = UnityEngine.Random.Range(1, _intervalQueue.Peek().Wave.MaxObjectSpawns + 1);
            StartCoroutine(SpawnBullets(bulletAmount));

            if (_changeIntervalCondition())
            {
                _intervalQueue.Dequeue();
            }

            SetTimer();
        }
    }

    private IEnumerator SpawnBullets(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float spawnX = UnityEngine.Random.Range(0.05f, 0.95f);
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(spawnX, 1.1f, 0));
            FallingObject @object = Instantiate(_intervalQueue.Peek().Wave.GetRandomObject(), spawnPosition, Quaternion.identity);
            @object.SetObjectAmount(amount);
            _bulletsSpawned++;
            yield return new WaitForSeconds(0.15f * (amount - 1));
        }
    }

    private void SetTimer()
    {
        ObjectWave currentWave = _intervalQueue.Peek().Wave;
        _spawnTimer = UnityEngine.Random.Range(currentWave.MinTimeBetweenSpawns, currentWave.MaxTimeBetweenSpawns);
    }
}
