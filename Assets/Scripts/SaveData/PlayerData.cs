// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// public class PlayerData : MonoBehaviour
// {
//     public static PlayerData current;
//     
//     private ProgressTracker _ProgressTracker;
//     public List<string> loadedLevels = new List<string>();
//
//     private void Singleton()
//     {
//         DontDestroyOnLoad(this);
//         if (current != null)
//         {
//             if (current != this)
//             {
//                 Destroy(gameObject);  
//             }
//         }
//         else
//         {
//             current = this;
//         }
//     }
//     
//     private void Awake()
//     {
//         Singleton();
//         InitProgressTracker();
//     }
//
//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.N))
//         {
//             PlayerPrefs.DeleteAll();
//             _ProgressTracker.LoadProgress(this);
//         }
//
//         if (Input.GetKeyDown(KeyCode.O))
//         {
//             _ProgressTracker.SaveProgress();
//         }
//     }
//
//     private void InitProgressTracker()
//     {
//         Debug.Log("Inited");
//         _ProgressTracker = new ProgressTracker();
//         _ProgressTracker.LoadProgress(this);
//        
//     }
//
//     public bool TryLoadLevel(string levelName)
//     {
//         // Вывод содержимого словаря levels для отладки
//         Debug.Log("Levels dictionary contents:");
//         foreach (var kvp in _ProgressTracker.levels)
//         {
//             Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
//         }
//
//         LevelState levelState = _ProgressTracker.GetLevelState(levelName);
//         switch (levelState)
//         {
//             case LevelState.Unlocked:
//             {
//                 return true;
//             }
//             case LevelState.Locked:
//             {
//                 return false;
//             }
//             case LevelState.Completed:
//             {
//                 return true;
//             }
//             default:
//             {
//                 throw new ArgumentException("Level with name '" + levelName + "' not found.");
//             }
//         }
//     }
//
//     public void UpdateLevelProgress()
//     {
//         var currentLevel = SceneManager.GetActiveScene().name;
//         _ProgressTracker.UpdateLevelState(currentLevel, LevelState.Completed);
//         
//         var nextLevel = _ProgressTracker.GetNextLevel(currentLevel);
//         if (nextLevel != null)
//         {
//             _ProgressTracker.UpdateLevelState(nextLevel.levelName, LevelState.Unlocked);
//         }
//         _ProgressTracker.SaveProgress();
//     }
//     
//     private void OnApplicationQuit()
//     {
//        
//     }
// }
