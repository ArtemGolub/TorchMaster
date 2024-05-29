using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("Debugging")] [SerializeField] private bool initDataIfNull = false;

    [Header("File Storage Config")] [SerializeField]
    private string fileName;

    [SerializeField] private ProgressionSO progress;

    private GameData _gameData;
    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandler _dataHandler;
    private string _selectedProfileId = "";
    public static DataPersistanceManager current { get; private set; }

    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataPersistanceObjects = FindAllDataPersistances();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void NewGame()
    {
        _gameData = new GameData();

        for (int i = 0; i < progress.LevelName.Count; i++)
        {
            if (_gameData.Levels.ContainsKey(progress.LevelName[i]))
            {
                _gameData.Levels[progress.LevelName[i]] = progress.LevelState[i];
            }
        }
        _gameData.Levels["Level1"] = LevelState.Unlocked;
        Debug.Log(_gameData.Levels["Level1"]);

        foreach (IDataPersistance obj in dataPersistanceObjects)
        {
            obj.LoadData(_gameData);
        }
    }

    public void LoadGame()
    {
        _gameData = _dataHandler.Load(_selectedProfileId);
        if (_gameData == null && initDataIfNull)
        {
            NewGame();
        }

        if (_gameData == null)
        {
            Debug.Log("No data to load");
            return;
        }

        foreach (IDataPersistance obj in dataPersistanceObjects)
        {
            obj.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        if (_gameData == null)
        {
            return;
        }

        foreach (IDataPersistance obj in dataPersistanceObjects)
        {
            obj.SaveData(ref _gameData);
        }

        _dataHandler.Save(_gameData, _selectedProfileId);
    }

    private List<IDataPersistance> FindAllDataPersistances()
    {
        IEnumerable<IDataPersistance> dataPersistances = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistances);
    }

    public bool HasGameData()
    {
        return _gameData == null;
    }

    public void ChangeSelectedProfileID(string newProfileId)
    {
        _selectedProfileId = newProfileId;
        LoadGame();
    }

    public Dictionary<string, GameData> GetAllProfilesData()
    {
        return _dataHandler.LoadAllProfiles();
    }

    public void CompleteLevel(string currentLevelName)
    {
        _gameData.Levels[currentLevelName] = LevelState.Completed;
        int currentLevelIndex = progress.LevelName.IndexOf(currentLevelName);
        if (currentLevelIndex < progress.LevelName.Count - 1)
        {
            string nextLevelName = progress.LevelName[currentLevelIndex + 1];
            _gameData.Levels[nextLevelName] = LevelState.Unlocked;
        }
        SaveGame();
        //SceneManager.LoadScene("MainMenu");
    }

    public string GetNextLevel(string currentLevelName)
    {
        int currentLevelIndex = progress.LevelName.IndexOf(currentLevelName);
        if (currentLevelIndex < progress.LevelName.Count - 1)
        {
            string nextLevelName = progress.LevelName[currentLevelIndex + 1];
            return nextLevelName;
        }
        else
        { 
           return null;
        }
    }
}