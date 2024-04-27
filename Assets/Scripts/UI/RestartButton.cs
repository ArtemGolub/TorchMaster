using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public static RestartButton current;
    
    public Canvas restartCanvas;
    public Button restartButton;
    public Button toMainMenu;

    
    private void Start()
    {
        current = this;
        restartButton.onClick.AddListener(RestartGame);
        toMainMenu.onClick.AddListener(ToMainMenu);
    }

    public void OpenRestartCanvas()
    {
        restartCanvas.enabled = true;
    }
    
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
