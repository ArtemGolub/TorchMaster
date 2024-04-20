using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[SerializeField]
public class ProgressTracker
{
    private Dictionary<string, LevelState> levels;

    public bool IsLevelDataEmpty()
    {
        return levels == null || levels.Count == 0;
    }
    public ProgressTracker()
    {
        LoadProgress();
    }
    public void SaveProgress()
    {
        Debug.Log("Saved");
        string json = JsonUtility.ToJson(levels);
        PlayerPrefs.SetString("progress", json);
        PlayerPrefs.Save();
    }
    
    private void LoadProgress()
    {
        if (PlayerPrefs.HasKey("progress"))
        {
            string json = PlayerPrefs.GetString("progress");
            levels = JsonUtility.FromJson<Dictionary<string, LevelState>>(json);
            Debug.Log("Loaded Progress");
            foreach (var levelName in levels.Keys.ToList())
            {
                if (!SceneManager.GetSceneByName(levelName).IsValid())
                {
                    Debug.LogWarning($"Level '{levelName}' does not exist. Removing from progress.");
                    levels.Remove(levelName);
                }
            }
        }
        else
        {
            levels = new Dictionary<string, LevelState>();
            AddLevel("Prototype2", LevelState.Unlocked);
            AddLevel("Prototype1", LevelState.Locked);
            
            Debug.Log("New Data Created");
        }
    }
    private void AddLevel(string levelName, LevelState initialState = LevelState.Locked)
    {
        levels.Add(levelName, initialState);
    }

    public void UpdateLevelState(string levelName, LevelState newState)
    {
        if (levels.ContainsKey(levelName))
        {
            levels[levelName] = newState;
        }
        else
        {
            Debug.LogWarning("Level not found: " + levelName);
        }
    }

    public LevelState GetLevelState(string levelName)
    {
        if (levels.ContainsKey(levelName))
        {
            return levels[levelName];
        }
        throw new ArgumentException("Level with name '" + levelName + "' not found.");
    }

    public LevelData GetNextLevel(string currentLevel)
    {
        int currentIndex = Array.IndexOf(levels.Keys.ToArray(), currentLevel);
        if (currentIndex == -1 || currentIndex == levels.Count - 1)
        {
            Debug.Log("This is the last level");
            return null;
        }
    
        string[] keysArray = levels.Keys.ToArray();
        string nextLevelKey = keysArray[currentIndex + 1];
        if (levels.TryGetValue(nextLevelKey, out LevelState nextState))
        {
            return new LevelData(nextLevelKey, nextState);
        }
        else
        {
            Debug.LogWarning("Next level not found.");
            return null;
        }
    }
}