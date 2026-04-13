using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Bomb,
    Heart,
    Stone
}

[System.Serializable]
public class FallingObjectFactory
{
    [SerializeField] private FallingObject[] _prefabs;

    private readonly Dictionary<ObjectType, FallingObject> _objectDictionary = new();

    public void Init()
    {
        foreach (var obj in _prefabs)
        {
            if (!_objectDictionary.ContainsKey(obj.Type))
            {
                _objectDictionary.Add(obj.Type, obj);
            }
        }
    }

    public FallingObject CreateObject(ObjectType type)
    {
        if (_objectDictionary.Count == 0)
        {
            Debug.LogError("FallingObjectFactory not initialized. Initializing.");
            Init();
        }

        if (_objectDictionary.TryGetValue(type, out var prefab))
        {
            float spawnX = UnityEngine.Random.Range(0.05f, 0.95f);
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(spawnX, 1.1f, 0));
            FallingObject @object = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            return @object;
        }
        else
        {
            Debug.LogError($"Object type {type} not found in factory.");
            return null;
        }
    }
}
