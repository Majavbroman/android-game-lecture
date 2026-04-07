using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class DataHandler : Singleton<DataHandler>
{
    private readonly List<IDataSaver> dataSavers = new List<IDataSaver>();

    private SaveData _saveData;

    private const string SAVE_FILE_PATH = "SavedData/SaveData.json";
    protected override void Awake()
    {
        base.Awake();

        #if UNITY_ANDROID
            // Request write permission if needed (older Android)
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }
        #endif

        LoadOrCreateJson();
    }

    #region Saving and Loading JSON
    private void SaveJson(SaveData saveData)
    {
        string folder = Path.Combine(GetSavePath(), "SavedData");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string pathFull = Path.Combine(folder, "SaveData.json");
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(pathFull, json);
        Debug.Log("Saved JSON: " + pathFull);
    }

    private void LoadOrCreateJson()
    {
        string pathFull = Path.Combine(GetSavePath(), SAVE_FILE_PATH);
        if (File.Exists(pathFull))
        {
            string json = File.ReadAllText(pathFull);
            _saveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Loaded JSON: " + pathFull);
        }
        else
        {
            _saveData = new SaveData();
            SaveJson(_saveData);
            Debug.Log("Created new JSON: " + pathFull);
        }
    }

    private string GetSavePath()
    {
        if (Application.isEditor)
            return Application.dataPath; // Editor folder
        else
            return Application.persistentDataPath; // Android, iOS, Standalone builds
    }
    #endregion

    #region Registering Data Savers
    public void Register(IDataSaver saver)
    {
        if (!dataSavers.Contains(saver))
            dataSavers.Add(saver);
    }

    public void Deregister(IDataSaver saver)
    {
        if (dataSavers.Contains(saver))
            dataSavers.Remove(saver);
    }
    #endregion

    #region Save Methods
    public async void SaveDataAsync()
    {
        foreach (IDataSaver saver in dataSavers)
        {
            await saver.SaveData(ref _saveData);
        }
        SaveJson(_saveData);
    }

    public void SaveData()
    {
        foreach (IDataSaver saver in dataSavers)
        {
            saver.SaveData(ref _saveData);
        }
        SaveJson(_saveData);
    }

    public SaveData GetData() => _saveData;
    #endregion

    public void OnNewGame()
    {
        _saveData.GameData = new GameData();
    }
}
