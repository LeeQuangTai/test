using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class DataSaverManager : MonoBehaviour
{
    public DataSaverManager instance { get; private set; }
    private GameData _gameData;
    private List<IDataSaver> dataSaverObjects;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than 1 DataSaverManager in the Scene!");
        }
        instance = this; 
    }

    private void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        this._gameData = new GameData();
    }

    public void LoadGame()
    {
        if (this._gameData == null)
        {
            Debug.Log("No data found! Initializing data to default!");
            NewGame(); 
        }

        foreach (IDataSaver dataSaverObject in dataSaverObjects)
        {
            dataSaverObject.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        // ToDo : pass the data other scripts so they can update it
        // ToDo : save that data to a file using data handler
        foreach (IDataSaver dataSaverObject in dataSaverObjects)
        {
            dataSaverObject.SaveData(ref _gameData);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataSaver> findAllDataSaversObjects()
    {
        IEnumerable<IDataSaver> dataSaversObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataSaver>();
        return new List<IDataSaver>(dataSaversObjects);
    }
}
