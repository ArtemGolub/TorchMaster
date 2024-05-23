using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndZone: MonoBehaviour, IEndZone
{
    public Canvas winCanvas;
    public Button menu;
    public Button nextLevel;

    private void Start()
    {
        winCanvas.enabled = false;
    }

    public void OnWin()
    {
        winCanvas.enabled = true;
        menu.onClick.AddListener(LoadMainMenu);
        
        string NextLevelName = DataPersistanceManager.current.GetNextLevel(SceneManager.GetActiveScene().name);
        if (NextLevelName == null)
        {
            nextLevel.gameObject.SetActive(false);
        }
        else
        {
            nextLevel.onClick.AddListener(ToNextLevel);
        }
    }
    
    
    private void ToNextLevel()
    {
        string NextLevelName = DataPersistanceManager.current.GetNextLevel(SceneManager.GetActiveScene().name);
        if (NextLevelName == null)
        {
            nextLevel.gameObject.SetActive(false);
            return;
        }
        LoadingScreenController.current.LoadNewScene(NextLevelName);
    }

    private void LoadMainMenu()
    {
        var LoadingScreenController = FindObjectOfType<LoadingScreenController>();
        LoadingScreenController.LoadNewScene("MainMenu");
    }

}