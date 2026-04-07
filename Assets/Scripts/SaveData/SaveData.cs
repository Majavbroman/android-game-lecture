using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public GameData GameData = null;
    public ScoreData ScoreData;

    public bool SavedGameData(out GameData gameData)
    {
        gameData = GameData;
        return GameData != null;
    }
}
